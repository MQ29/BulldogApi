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
        public Guid EmployeeId { get; protected set; }
        public DateTime? Date { get; protected set; }
        public bool IsFinsished { get; protected set; }//unfinished false, finished true

        protected Reservation()
        {
            
        }

        public Reservation(string userId, Guid serviceId, Guid employeeId, DateTime? date)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ServiceId = serviceId;
            EmployeeId = employeeId;
            Date = date;
            IsFinsished = false;
        }
    }
}
