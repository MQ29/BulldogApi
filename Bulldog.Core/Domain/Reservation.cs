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
        public Service Service { get; protected set; }
        public Employee Employee { get; protected set; }
        public DateTime Date { get; protected set; }
        public bool Status { get; protected set; }

    }
}
