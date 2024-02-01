using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string StreetAndHouseNumber { get; set; }
        public int ApartNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
