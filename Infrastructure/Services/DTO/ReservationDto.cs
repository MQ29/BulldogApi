using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services.DTO
{
    public class ReservationDto
    {
        public Guid Id { get;  set; }
        public string UserId { get; set; }
        public Guid ServiceId { get;  set; }
        public Guid EmployeeId { get;  set; }
        public DateTime Date { get;  set; }
        public bool IsFinsished { get;  set; }
    }
}
