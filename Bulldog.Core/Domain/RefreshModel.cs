using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class RefreshModel
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
