using Bulldog.Infrastructure.Services.DTO;
using BulldogApiFrontend.Services.IServices;
using System.Net.Http.Json;

namespace BulldogApiFrontend.Services
{
    public class ServiceService : IServiceService
    {
        private readonly HttpClient _httpClient;
        public ServiceService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }
        public async Task<IList<ServiceDto>> GetServicesByCompanyId(Guid companyId)
        {
            var response = await _httpClient.GetAsync($"api/services/companies/{companyId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var services = await response.Content.ReadFromJsonAsync<IList<ServiceDto>>();
            return services;
        }
    }
}
