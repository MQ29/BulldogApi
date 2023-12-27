using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Domain
{
    public class Employee
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public IEnumerable<Service> Services { get; protected set; }

        protected Employee()
        {
            
        }

        public Employee(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }


    }

  
}
