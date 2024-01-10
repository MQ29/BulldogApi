using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class EFServiceRepository : IServiceRepository
    {
        private readonly BulldogDbContext _context;

        public EFServiceRepository(BulldogDbContext context)
        {
            _context = context;
        }
        public void Add(Service service)
        {
            var serviceToFind = _context.Services.FirstOrDefault(x => x.Id == service.Id);
            if (serviceToFind != null)
            {
                throw new Exception("Service already exists");
            }
            else
            {
                _context.Add(service);
                _context.SaveChanges();
            }
        }

        public Service Get(Guid Id)
        {
            var service = _context.Services.FirstOrDefault(x => x.Id == Id);
            if (service != null)
            {
                return service;
            }

            throw new Exception($"Service with id: {Id} not found."); 
        }

        public Service GetByEmployeeId(Guid employeeId)
        {
            var service = _context.Services.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (service != null)
            {
                return service;
            }

            throw new Exception($"Service with EmployeeId: {employeeId} not found.");
        }

        public Service Get(string name)
        {
            var service = _context.Services.FirstOrDefault(x => x.Name == name);
            if (service != null)
            {
                return service;
            }

            throw new Exception($"Service with name: {name} not found.");
        }

        public void Remove(Service service)
        {
            _context.Remove(service);
            _context.SaveChanges();
        }

        public void Update(Service service)
        {
            _context.Update(service);
            _context.SaveChanges();
        }
    }
}
