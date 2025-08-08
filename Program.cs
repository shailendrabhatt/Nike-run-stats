using Nike_DataExtraction.Models;
using Nike_DataExtraction.Services;

var builder = WebApplication.CreateBuilder(args);

// Path to folder containing your result1.json etc.
var runFolderPath = @""; // <-- set your path here

builder.Services.AddSingleton(new RunDataService(runFolderPath));
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<OpenAIChatService>();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
