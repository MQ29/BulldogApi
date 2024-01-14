using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class AvailableDate
    {
        public Guid Id { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime EndTime { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Color { get; protected set; }
        public Guid EmployeeId { get; protected set; }

        protected AvailableDate()
        {
            
        }
        public AvailableDate(Employee employee, DateTime startTime, DateTime endTime,
            string title, string description, string color)
        {
            Id = Guid.NewGuid();
            EmployeeId = employee.Id;
            StartTime = startTime;
            EndTime = endTime;
            Title = title;
            Description = description;
            Color = color;
        }
    }
}
