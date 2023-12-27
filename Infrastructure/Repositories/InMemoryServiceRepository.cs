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
        
        public void Add(Service service)
        {
            _services.Add(service);
        }

        public Service Get(Guid Id)
            => _services.SingleOrDefault(x => x.Id == Id);

        public Service Get(string name)
            => _services.SingleOrDefault(x => x.Name == name);

        public void Remove(Service service)
        {
        }

        public void Update(Service service)
        {
        }
    }
}
