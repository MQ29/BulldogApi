using Blazored.LocalStorage;
using BulldogUI.Client.Handlers;
using BulldogUI.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>();
builder.Services.AddScoped<IApiEmployeeService, ApiEmployeeService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorageAsSingleton();


await builder.Build().RunAsync();
