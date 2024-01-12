using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class InMemoryServiceRepository : IServiceRepository
    {
        private static ISet<Service> _services = new HashSet<Service>();

        public Task AddAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Service>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetByEmployeeIdAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Service service)
        {
            throw new NotImplementedException();
        }

        Task<IList<Service>> IServiceRepository.GetByEmployeeIdAsync(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
