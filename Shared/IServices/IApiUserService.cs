using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.IServices
{
    public interface IApiUserService
    {
        Task<UserDto> GetUserByEmail(string email);
    }
}
