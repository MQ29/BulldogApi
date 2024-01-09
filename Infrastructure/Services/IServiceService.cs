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
        void Create(string name, decimal price, int duration);
        ServiceDto Get(string name);
        ServiceDto Get(Guid employeeId);


    }
}
