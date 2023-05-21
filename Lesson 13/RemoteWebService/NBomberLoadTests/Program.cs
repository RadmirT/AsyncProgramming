using System.Text.Json;
using System.Text.RegularExpressions;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Contracts.Stats;

if (args.Length is < 1 or > 2 || args[0] is not ("sync" or "async" or "both"))
{
    Console.Error.WriteLine("Usage: dotnet run --project NBomberLoadTests -- sync|async|both [baseUrl]");
    return 1;
}

var baseUrl = args.Length > 1 ? args[1] : "http://localhost:44336";
if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri) ||
    baseUri.Scheme is not ("http" or "https"))
{
    Console.Error.WriteLine("baseUrl must be an absolute HTTP(S) URL.");
    return 1;
}

var homePage = new Uri(baseUri, "/Home/Index");
var requestTimeout = TimeSpan.FromSeconds(300);

const int usersPerStep = 25;
const int maxUsers = 1000;
const int stepSeconds = 3;
const int testSeconds = 240;
const int reportingSeconds = 10;
const int rampSteps = maxUsers / usersPerStep - 1;
var load = Enumerable.Range(1, rampSteps)
    .Select(level => Simulation.KeepConstant(level * usersPerStep, TimeSpan.FromSeconds(stepSeconds)))
    .Append(Simulation.KeepConstant(maxUsers, TimeSpan.FromSeconds(testSeconds - rampSteps * stepSeconds)))
    .ToArray();

NodeStats RunScenario(string name)
{
    using var client = new HttpClient { Timeout = requestTimeout };
    var endpoint = new Uri(baseUri, name == "sync" ? "/Home/LoadSync" : "/Home/LoadAsync");
    var scenario = Scenario.Create(name, async context =>
    {
        // The first visit opens the home page. Later visits alternate between the two routes.
        var uri = context.InvocationNumber % 2 == 1 ? homePage : endpoint;
        return await Step.Run(uri.AbsolutePath, context, async () =>
        {
            try
            {
                // Let requests already sent finish when NBomber stops scheduling iterations.
                using var response = await client.GetAsync(uri);
                var statusCode = ((int)response.StatusCode).ToString();
                return response.IsSuccessStatusCode
                    ? Response.Ok(statusCode: statusCode)
                    : Response.Fail(statusCode: statusCode, message: response.ReasonPhrase ?? "HTTP error");
            }
            catch (OperationCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                return Response.Fail(statusCode: "http-timeout", message: $"No response within {requestTimeout.TotalSeconds} s");
            }
        });
    })
        .WithRestartIterationOnFail(false)
        .WithWarmUpDuration(TimeSpan.FromSeconds(15))
        .WithLoadSimulations(load);

    return NBomberRunner.RegisterScenarios(scenario)
        .WithReportingInterval(TimeSpan.FromSeconds(reportingSeconds))
        .WithScenarioCompletionTimeout(requestTimeout + TimeSpan.FromSeconds(10))
        .WithReportFolder(Path.Combine(AppContext.BaseDirectory, "reports"))
        .WithReportFileName(name)
        .Run();
}

if (args[0] != "both")
{
    RunScenario(args[0]);
    return 0;
}

var sync = RunScenario("sync").ReportFiles.First(file => file.ReportFormat == ReportFormat.Html);
var asyncReport = RunScenario("async").ReportFiles.First(file => file.ReportFormat == ReportFormat.Html);
var data = JsonSerializer.Serialize(new
{
    sync = ReadTimeline(sync.ReportContent),
    async = ReadTimeline(asyncReport.ReportContent),
    usersPerStep,
    maxUsers,
    stepSeconds,
    rampSeconds = rampSteps * stepSeconds,
    reportingSeconds
});

using var resource = typeof(Program).Assembly.GetManifestResourceStream("Comparison.html")!;
using var reader = new StreamReader(resource);
var comparisonPath = Path.Combine(Path.GetDirectoryName(sync.FilePath)!, "sync-vs-async.html");
File.WriteAllText(comparisonPath, reader.ReadToEnd().Replace("/* REPORT_DATA */", data, StringComparison.Ordinal));
Console.WriteLine($"Comparison report: {comparisonPath}");
return 0;

static JsonElement ReadTimeline(string html)
{
    // NBomber 6.7.1 embeds the interval statistics as JSON in its HTML report.
    var match = Regex.Match(html, @"(?m)^\s*""TimeLineHistory"":\s*(\[.*\]),\s*$");
    if (!match.Success)
        throw new InvalidDataException("NBomber HTML report has no timeline data.");

    return JsonSerializer.Deserialize<JsonElement>(match.Groups[1].Value);
}
