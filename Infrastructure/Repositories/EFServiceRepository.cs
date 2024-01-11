using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public async Task AddAsync(Service service)
        {
            var serviceToFind = _context.Services.FirstOrDefault(x => x.Id == service.Id);
            if (serviceToFind != null)
            {
                throw new Exception("Service already exists");
            }
            else
            {
                await _context.AddAsync(service);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetByIdAsync(Guid Id)
        {
            var service = _context.Services.FirstOrDefault(x => x.Id == Id);
            if (service != null)
            {
                return service;
            }

            throw new Exception($"Service with id: {Id} not found.");
        }

        public async Task<Service> GetByEmployeeIdAsync(Guid employeeId)
        {
            var service = _context.Services.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (service != null)
            {
                return service;
            }

            throw new Exception($"Service with EmployeeId: {employeeId} not found.");
        }

        public Task RemoveAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
