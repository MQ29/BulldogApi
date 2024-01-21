using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services.DTO
{
    public  class AvailableHourDto
    {
        public Guid Id { get; set; }
        public TimeSpan Hour { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
