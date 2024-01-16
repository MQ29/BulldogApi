using Blazored.Modal;
using BulldogApiFrontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BulldogApiFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7112") });
            builder.Services.AddScoped<IServiceApiService, ServiceApiService>();
            builder.Services.AddBlazoredModal();
            await builder.Build().RunAsync();
        }
    }
}
