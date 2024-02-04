using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Shared.IServices;
using Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<IApiEmployeeService, ApiEmployeeService>();
builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddHttpClient("ServerApi")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7112"))
    .AddHttpMessageHandler<AuthenticationHandler>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IApiUserService, ApiUserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddBlazoredLocalStorage();
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddMudServices();
var app = builder.Build();
await builder.Build().RunAsync();
