using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services.DTO
{
    public  class CompanyDto
    {
        public Guid Id { get; set; }
        public string? Name { get;  set; }
        public Address? Address { get;  set; }
        public string? Description { get;  set; }
        public string? PhoneNumber { get;  set; }
        public IList<Employee> Employees { get;  set; } = new List<Employee>();
        public IList<Opinion> Opinions { get;  set; } = new List<Opinion>();
    }
}
