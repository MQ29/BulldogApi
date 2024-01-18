using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services.DTO
{
    public class AvailableDateDto
    {
        public Guid Id { get;  set; }
        public bool IsOpen { get; set; }
        public DayOfWeek DayOfWeek { get;  set; }
        public List<Break> Breaks { get;  set; }
        public WorkingHours WorkingHours { get; set; }
        public Guid EmployeeId { get;  set; }
    }
}
