using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Reservation
    {
        public Guid Id { get; protected set; }
        public string UserId { get; protected set; }
        public Guid ServiceId { get; protected set; }
        public string ServiceName { get; set; }
        public Guid EmployeeId { get; protected set; }
        public DateTime DateOfReservation { get; set; }
        public DateTime? StartDate { get; protected set; }
        public DateTime FinishDate { get; set; }
        public bool IsFinsished { get; protected set; }//unfinished false, finished true

        protected Reservation()
        {
            
        }

        public Reservation(string userId, Guid serviceId, string serviceName, Guid employeeId, DateTime? startDate, DateTime finishDate)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ServiceId = serviceId;
            ServiceName = serviceName;
            EmployeeId = employeeId;
            StartDate = startDate;
            FinishDate = finishDate;
            IsFinsished = false;
            DateOfReservation = DateTime.UtcNow;
        }
    }
}
