using Bulldog.Infrastructure.Services.DTO;
using Shared.IServices;
using System.Net.Http;
using System.Net.Http.Json;

namespace Shared.Services
{
    public class ApiEmployeeService : IApiEmployeeService
    {
        private readonly HttpClient _httpClient;
        public ApiEmployeeService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }
        public async Task<IList<EmployeeDto>> GetAllEmployees()
        {
            return await _httpClient.GetFromJsonAsync<IList<EmployeeDto>>("employees/all");
        }
    }
}
