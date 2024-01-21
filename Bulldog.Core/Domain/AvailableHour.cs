using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class AvailableHour
    {
        public Guid Id { get; protected set; }
        public DateTime Hour { get; set; }
        public Guid EmployeeId { get; protected set; }
        public bool IsAvailable { get; set; } 

        public AvailableHour(Guid employeeId, DateTime hour)
        {
            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            Hour = hour;
            IsAvailable = true;
        }

    }
}
