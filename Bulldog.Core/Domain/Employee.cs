using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Employee
    {
        private ICollection<Service> _services = new List<Service>();
        public Guid Id { get; protected set; }
        public string UserId { get; protected set; }
        public string Email { get; protected set; }
        public string? Name { get; protected set; }
        //public string? CompanyName { get; set; }
        public IEnumerable<Service> Services
        {
            get { return _services; }
            set { _services = new List<Service>(value); }
        }
        public ICollection<AvailableHour>? AvailableHours { get; set; } = new List<AvailableHour>();
        public ICollection<AvailableDate> AvailableDates { get; set; } = new List<AvailableDate>();
        protected Employee()
        {
            
        }

        public Employee(User user)
        {
            Id = Guid.NewGuid();
            Name = user.Fullname;
            UserId = user.Id;
            Email = user.Email;
        }
        public void GenerateAvailableHours(DateTime date)
        {
            var availableDate = AvailableDates.FirstOrDefault(x => x.DayOfWeek == date.DayOfWeek);
            DateTime Interval = date + availableDate.WorkingHours.StartTime;

            while (Interval < DateTime.Today + availableDate.WorkingHours.EndTime)
            {
                AvailableHours.Add(new AvailableHour(Id, Interval));
                Interval = Interval.AddMinutes(15);
            }
        }
        public void AddAvailableDate(AvailableDate availableDate)
        {
            AvailableDates.Add(availableDate);
        }

        public void AddAvailableHour(AvailableHour availableHour)
        {
            AvailableHours.Add(availableHour);
        }

        public void ClearAvailableDates()
        {
            foreach (var availableDate in AvailableDates)
            {
                AvailableDates.Remove(availableDate);
            }
        }

        public void AddService(string name, decimal price, int duration, Employee employee)
        {
            var service = Services.FirstOrDefault(x => x.Name == name);
            if (service != null)
            {
                throw new Exception($"Service with name: '{name}' already exists for employee: {Name}.");
            }
            _services.Add(Service.Create(name, price, duration, employee));
        }

        public void DeleteService(string name)
        {
            var service = Services.FirstOrDefault(x => x.Name == name);
            if (service == null)
            {
                throw new Exception($"Service with name: '{name}' for employee: {Name} was not found.");
            }
            _services.Remove(service);
        }

    }

  
}
