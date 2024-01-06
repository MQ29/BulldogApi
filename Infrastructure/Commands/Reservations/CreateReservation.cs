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
        public Service Service { get; protected set; }
        public Employee Employee { get; protected set; }
        public DateTime Date { get; protected set; }
        public bool Status { get; protected set; }
    }
}
