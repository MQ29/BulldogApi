using Blazored.Modal;
using BulldogApiFrontend.Identity;
using BulldogApiFrontend.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace BulldogApiFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after"); //cotojest
            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddBlazoredModal();
            builder.Services.AddScoped<CookieHandler>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
            builder.Services.AddScoped(
sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7112") });
            builder.Services.AddScoped<IServiceApiService, ServiceApiService>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddHttpClient(
    "Auth",
    opt => opt.BaseAddress = new Uri(builder.Configuration["AuthUrl"]!))
    .AddHttpMessageHandler<CookieHandler>();
            await builder.Build().RunAsync();
        }
    }
}
