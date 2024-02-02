using Microsoft.AspNetCore.Components.Authorization;

namespace BulldogApiFrontend.Models
{
    public class CustomAuthProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
