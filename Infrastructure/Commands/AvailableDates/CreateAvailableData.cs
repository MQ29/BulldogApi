using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Commands.AvailableDates
{
    public class CreateAvailableDate : ICommand
    {
        public bool IsOpen { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<Break> Breaks { get; set; }
        public WorkingHours WorkingHours { get; set; }
    }
}
