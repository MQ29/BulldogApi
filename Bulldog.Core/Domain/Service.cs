﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Service
    {
        public Guid Id { get; protected set; }
        public Guid EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public int Duration { get; protected set; }

        protected Service()
        {

        }
        public Service(string name, decimal price, int duration, Employee employee)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Duration = duration;
            EmployeeId = employee.Id;
            CompanyId = employee.CompanyId;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty");
            if (Name == name)
                return;
            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new Exception("Price can't be a negative number");
            if (Price == price)
                return;
            Price = price;
        }

        public void SetDuration(int duration)
        {
            if (Duration == duration)
                return;
            Duration = duration;
        }

        public static Service Create(string name, decimal price, int duration, Employee employee)
            => new Service(name, price, duration, employee);
    }
}
