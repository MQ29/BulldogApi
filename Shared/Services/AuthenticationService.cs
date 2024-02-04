using Blazored.LocalStorage;
using Bulldog.Core.Domain;
using Shared.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Shared.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _factory;
        private ILocalStorageService _localStorageService;
        private string JWT_KEY = nameof(JWT_KEY);
        private string? _jwtCache;
        public AuthenticationService(IHttpClientFactory factory, ILocalStorageService localStorageService)
        {
            _factory = factory;
            _localStorageService = localStorageService;
        }

        public event Action<string?>? LoginChange;

        public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = await _localStorageService.GetItemAsync<string>(JWT_KEY);

            return _jwtCache;
        }

        public async Task<DateTime> LoginAsync(LoginModel model)
        {
            var response = await _factory.CreateClient("ServerApi").PostAsJsonAsync("Authenticate/login", model);

            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException("Login failed.");
            }

            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (content is null)
            {
                throw new InvalidDataException();
            }

            await _localStorageService.SetItemAsync(JWT_KEY, content.JwtToken);

            LoginChange?.Invoke(GetUsername(content.JwtToken));

            return content.Expiration;
        }

        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItemAsync(JWT_KEY);

            _jwtCache = null; 

            LoginChange?.Invoke(null);
        }

        private static string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);
            return jwt.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        }

        public Task<bool> RefreshAsync()
        {
            throw new NotImplementedException();
        }

    }
}
