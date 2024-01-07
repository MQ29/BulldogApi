using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Commands.Reservations
{
    public class CreateReservation : ICommand
    {
        public Guid ServiceId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
