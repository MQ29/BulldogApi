using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services.DTO
{
    public class UpdateCompanyDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
    }
}
