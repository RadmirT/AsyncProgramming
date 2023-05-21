var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(_ => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["PeopleApiBaseUrl"] ?? "http://localhost:5279/")
});
builder.Services.AddSingleton<SyncVSAsync.Models.PersonClient>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
