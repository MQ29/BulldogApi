using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.SessionStorage;
using BulldogApiFrontend.Handlers;
using BulldogApiFrontend.Models;
using BulldogApiFrontend.Pages;
using BulldogApiFrontend.Services;
using BulldogApiFrontend.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
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
            builder.Services.AddTransient<AuthenticationHandler>();
            builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ServerUrl"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>();
            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddBlazoredModal();
            builder.Services.AddScoped<IServiceApiService, ServiceApiService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddBlazoredLocalStorageAsSingleton();
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
