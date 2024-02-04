using Microsoft.Extensions.Configuration;
using Shared.IServices;
using System.Net.Http.Headers;

namespace Shared.Services
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationHandler(IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _authenticationService.GetJwtAsync();
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["SeverUrl"] ?? "") ?? false;

            if (isToServer && !string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
