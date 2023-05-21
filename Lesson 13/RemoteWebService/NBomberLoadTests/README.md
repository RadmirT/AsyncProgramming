# NBomber load tests

Start `RemoteWebService` on `http://localhost:5279` and `001_SyncVSAsync` on `http://localhost:44336` first. Then run the comparison from the `RemoteWebService` directory:

```powershell
dotnet run -c Release --project NBomberLoadTests -- both
```

To run only one scenario, use:

```powershell
dotnet run -c Release --project NBomberLoadTests -- sync
dotnet run -c Release --project NBomberLoadTests -- async
```

An optional second argument overrides the web application's base URL. The `both` mode runs sync first, waits for its requests to finish, then runs async with the same profile and a fresh warm-up. It creates a comparison report and links to both individual NBomber reports.

NBomber writes reports to `bin/Release/net10.0/reports` in the load test project and prints the path during the run.

Open `sync-vs-async.html` in a browser. The **Sync vs async** section overlays the two load endpoints in charts for p95 response time and successful requests per second, using the same 5-second intervals. Hover over a point to compare both values. Click a legend entry to hide or show that line.

Each virtual user opens `/Home/Index` first, then alternates between the selected load endpoint and the home page. The tests use 25 to 1000 concurrent users in increments of 25 every 3 seconds, 15 seconds of warm-up, and 120 seconds of measured load per run. NBomber reports request timings and errors separately for each route.

Both MVC endpoints reuse a single `HttpClient` and call the same people API. The sync endpoint blocks in `Send`, and the async endpoint awaits `SendAsync`. The API uses an asynchronous 3-second delay.

After the load phase, NBomber waits for requests already sent to finish. Each HTTP request has a 300-second timeout, which is reported as `http-timeout`.

The recorded requests have zero think time. The NBomber tests fetch the HTML pages. The Visual Studio tests also loaded dependent resources such as CSS and scripts.
