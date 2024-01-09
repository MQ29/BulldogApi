using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Bulldog.Infrastructure.Services.DTO;

namespace BulldogApiFrontend
{
    public class ServiceApiService
    {
        private readonly HttpClient _httpClient;

        public ServiceApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ServiceDto>> GetServies()
        {
            var response = await _httpClient.GetAsync("/api/services");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ServiceDto>>();
        }

        public async Task<ServiceDto> GetBook(int id)
        {
            var response = await _httpClient.GetStreamAsync($"/api/books/{id}");
            var book = await JsonSerializer.DeserializeAsync<ServiceDto>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return book;
        }

        public async Task<ServiceDto> CreateBook(ServiceDto bookForCreationDto) //serviceForCreation?
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/books", bookForCreationDto);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();

                    var addedDriver = await JsonSerializer.DeserializeAsync<ServiceDto>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return addedDriver;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    throw new Exception($"Error while creating a book. Status code: {response.StatusCode}");
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/books/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        //public async Task<bool> Update(ServiceDto BookDto)
        //{
        //    try
        //    {
        //        var bookToUpdate = _mapper.Map<BookForCreationDto>(BookDto);
        //        var itemJson = new StringContent(JsonSerializer.Serialize(bookToUpdate), Encoding.UTF8, "application/json");

        //        var response = await _httpClient.PutAsync($"api/books/{BookDto.Id}", itemJson);

        //        return response.IsSuccessStatusCode;
        //    }


        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        throw ex;
        //    }
        //}

    }
}
