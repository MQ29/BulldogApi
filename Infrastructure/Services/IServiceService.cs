using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public interface IServiceService
    {
        void Create(Guid Id, string name, decimal price, int duration, Guid employeeId);
        ServiceDto Get(string name);
        ServiceDto Get(Guid employeeId);


    }
}
