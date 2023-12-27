using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Client
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public string PhoneNumber { get; protected set; }

    }
}
