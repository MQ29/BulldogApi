using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class AvailableDate
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public Guid EmployeeId { get; set; }

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
