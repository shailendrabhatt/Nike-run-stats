using Nike_DataExtraction.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Nike_DataExtraction.Services
{
    public class RunDataService
    {
        private readonly string _folderPath;

        public RunDataService(string folderPath)
        {
            _folderPath = folderPath;
        }

        public List<RunDisplayModel> GetAllRuns()
        {
            var results = new List<RunDisplayModel>();
            if (!Directory.Exists(_folderPath))
                return results;

            var files = Directory.GetFiles(_folderPath, "result*.json");

            foreach (var file in files)
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var activitiesFile = JsonSerializer.Deserialize<NikeActivitiesFile>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (activitiesFile?.activities == null)
                        continue;

                    foreach (var run in activitiesFile.activities)
                    {
                        results.Add(ExtractRun(run));
                    }
                }
                catch
                {
                    // Optional: Log error or ignore
                }
            }
            return results;
        }

        private RunDisplayModel ExtractRun(NikeRunData run)
        {
            DateTime date = run.start_epoch_ms != null
                ? DateTimeOffset.FromUnixTimeMilliseconds(run.start_epoch_ms.Value).LocalDateTime
                : DateTime.MinValue;

            return new RunDisplayModel
            {
                Id = run.id,
                Date = date,
                Speed = GetSummary(run, "speed", "mean"),
                Pace = GetSummary(run, "pace", "mean"),
                Steps = GetSummary(run, "steps", "total"),
                Calories = GetSummary(run, "calories", "total"),
                Distance = GetSummary(run, "distance", "total"),
                Cadence = GetSummary(run, "cadence", "mean"),
                Location = run.tags != null && run.tags.ContainsKey("location") ? run.tags["location"] : "",
                Temperature = run.tags != null && run.tags.ContainsKey("com.nike.temperature") ? run.tags["com.nike.temperature"] : "",
                Latitude = null,   // Add if needed from moments or polyline
                Longitude = null
            };
        }

        private static double GetSummary(NikeRunData run, string metric, string summary)
        {
            return run?.summaries?.FirstOrDefault(x => x.metric == metric && x.summary == summary)?.value ?? 0;
        }
    }
}
