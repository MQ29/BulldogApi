using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class LoginResponse
    {
        public required string JwtToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
