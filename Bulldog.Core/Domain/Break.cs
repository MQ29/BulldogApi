using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Break
    {
        public Guid Id { get; protected set; }
        public TimeSpan StartTime { get; protected set; }
        public TimeSpan EndTime { get; protected set; }
        public Guid AvailableDateId { get; set; }
    }
}
