using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Repositories
{
    public interface IServiceRepository
    {
        Service Get(Guid Id);
        Service Get(string name);
        Service GetByEmployeeId(Guid employeeId);
        void Add(Service service);
        void Update(Service service);
        void Remove(Service service);
    }
}
