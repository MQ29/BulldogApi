using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bulldog.Core.Domain
{
    public class AvailableDate
    {
        public Guid Id { get; protected set; }
        public bool IsOpen { get; protected set; }
        public DayOfWeek DayOfWeek { get; protected set; }
        public ICollection<Break>? Breaks { get; set; } = new List<Break>();
        public WorkingHours? WorkingHours { get; set; }
        public Guid EmployeeId { get; protected set; }
        protected AvailableDate()
        {
            
        }
        public AvailableDate(Guid employeeId, DayOfWeek dayOfWeek, bool isOpen, WorkingHours workingHours)
        {
            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            DayOfWeek = dayOfWeek;
            IsOpen = isOpen;
            WorkingHours = workingHours;
        }

        public void AddBreaks(Break Break)
        {
            Breaks.Add(Break);
        }
    }
}
