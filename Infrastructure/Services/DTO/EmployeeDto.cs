using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services.DTO
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<ServiceDto> Services { get; set; }
    }
}
