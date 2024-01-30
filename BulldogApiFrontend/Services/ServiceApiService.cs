using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Bulldog.Infrastructure.Services.DTO;
using System.Collections.Generic;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Core.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Blazored.SessionStorage;

namespace BulldogApiFrontend.Services
{
    public class ServiceApiService : IServiceApiService
    {
        private readonly HttpClient _httpClient;

        public ServiceApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task<ServiceDto> Get(Guid Id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/services/{Id}");
                response.EnsureSuccessStatusCode();
                var service = await response.Content.ReadFromJsonAsync<ServiceDto>();
                return service;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving service by Id: {ex.Message}");
                throw;
            }
        }

        public async Task<IList<ServiceDto>> GetAllServies()
        {
            return await _httpClient.GetFromJsonAsync<IList<ServiceDto>>("api/services");
        }

        public async Task<IList<AvailableDateDto>> GetAvailableDates(Guid employeeId)
        {
            return await _httpClient.GetFromJsonAsync<IList<AvailableDateDto>>($"employees/{employeeId}/availableDates");
        }

        public async Task<IList<EmployeeDto>> GetAllEmployees()
        {
            return await _httpClient.GetFromJsonAsync<IList<EmployeeDto>>("employees/all");
        }

        //public async Task<EmployeeDto> GetEmployee(Guid Id);

        public async Task<IList<EmployeeDto>> GetEmployyesForServiceId(Guid Id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IList<EmployeeDto>>($"employees/by-service/{Id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employyes for service with Id: {ex.Message}");
                throw;
            }
        }

        public async Task AddAvailabilityDates(Guid EmployeeId, IList<AvailableDateDto> availableDates)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"employees/InsertAvailableDate/{EmployeeId}", availableDates);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();

                    var addedavailableDates = await JsonSerializer.DeserializeAsync<IList<AvailableDateDto>>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    throw new Exception($"Error while creating availabilityDates. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in service: {ex.Message}");
            }
        }

        public async Task<bool> UpdateAvailabilityDates(Guid EmployeeId, IList<AvailableDateDto> availableDates)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(availableDates), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"employees/UpdateAvailableDates/{EmployeeId}", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<IList<AvailableHour>> GetAvailableHoursForDay(Guid employeeId, DateTime date)
        {
            return await _httpClient.GetFromJsonAsync<IList<AvailableHour>>($"employees/{employeeId}/availableHours/{date.ToString("yyyy-MM-dd")}");
        }

        public async Task<IList<AvailableHour>> GetAllAvailableHours(Guid employeeId)
        {
            return await _httpClient.GetFromJsonAsync<IList<AvailableHour>>($"employees/{employeeId}/availableHours");
        }

        public async Task<EmployeeDto> GetByEmailAsync(string email)  
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDto>($"employees/{email}");
        }

        public async Task<EmployeeDto> GetByUserIdAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDto>($"employees/ByUserId/{userId}");
        }

        public async Task AddReservation(Reservation reservation)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"Reservations", reservation);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();

                    var addedReservation = await JsonSerializer.DeserializeAsync<ReservationDto>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    throw new Exception($"Error while creating reservation. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in service: {ex.Message}");
            }
        }

        public async Task Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Authenticate/register", registerModel);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Rejestracja nie powiodla sie");
            }
            var responseBody = await response.Content.ReadAsStreamAsync();
            var addedUser = await JsonSerializer.DeserializeAsync<RegisterModel>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> UpdateAvailabileHours(Guid employeeId, UpdateAvailableHours request)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"employees/UpdateAvailableHours/{employeeId}", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ServiceDto> GetServiceById(Guid serviceId)
        {
            return await _httpClient.GetFromJsonAsync<ServiceDto>($"api/services/{serviceId}");
        }

        public async Task<IList<ReservationDto>> GetReservations(Guid employeeId)
        {
            return await _httpClient.GetFromJsonAsync<IList<ReservationDto>>($"Reservations/{employeeId}");
        }
    }
}

