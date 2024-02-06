using Blazored.LocalStorage;
using BulldogUI.Client.Pages;
using BulldogUI.Components;
using MudBlazor.Services;
using Shared.IServices;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddHttpClient("ServerApi")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7112"))
    .AddHttpMessageHandler<AuthenticationHandler>();
builder.Services.AddScoped<IApiEmployeeService, ApiEmployeeService>();
builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IApiUserService, ApiUserService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Configuration.AddJsonFile("appsettings.json");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BulldogUI.Client._Imports).Assembly);

app.Run();
