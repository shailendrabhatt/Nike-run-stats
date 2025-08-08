
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nike_DataExtraction.Models
{
    public class NikeRunSummary
    {
        public string metric { get; set; }
        public string summary { get; set; }
        public double value { get; set; }
    }

    public class NikeRunMoment
    {
        public string key { get; set; }
        public string value { get; set; }
        public long timestamp { get; set; }
    }

    public class NikeRunData
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<NikeRunSummary>? summaries { get; set; }
        public Dictionary<string, string>? tags { get; set; }
        public List<NikeRunMoment>? moments { get; set; }
        public long? start_epoch_ms { get; set; }
        public long? end_epoch_ms { get; set; }
    }

    public class NikeActivitiesFile
    {
        public List<NikeRunData>? activities { get; set; }
    }

    public class RunDisplayModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }

        // Expose formatted DateTime for display in table
        public string DateTimeDisplay => Date.ToString("yyyy-MM-dd HH:mm");

        public double Speed { get; set; }
        public double Pace { get; set; }
        public double Steps { get; set; }
        public double Calories { get; set; }
        public double Distance { get; set; }
        public double Cadence { get; set; }
        public string Location { get; set; }
        public string Temperature { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public class RunMonthlyAggregate
    {
        public string YearMonth { get; set; }
        public int Count { get; set; }
        public double TotalDistance { get; set; }
        public double TotalCalories { get; set; }
        public double TotalSteps { get; set; }
        public double AvgSpeed { get; set; }
        public double AvgPace { get; set; }
    }
}