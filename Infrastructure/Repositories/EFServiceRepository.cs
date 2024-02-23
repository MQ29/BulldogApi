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
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == Id);
            if (service is null)
            {
                throw new Exception($"Service with id: {Id} not found.");
            }
            return service;
        }

        public async Task<IList<Service>> GetByEmployeeIdAsync(Guid employeeId)
        {
            var services = await _context.Services.Where(x => x.EmployeeId == employeeId).ToListAsync();
            return services;
            //throw new Exception($"Service with EmployeeId: {employeeId} not found.");
        }

        public Task UpdateAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Guid Id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Id == Id);
            if (service != null)
            {
                _context.Remove(service);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Service>> GetByCompanyId(Guid companyId)
        {
            return await _context.Services.Where(x => x.CompanyId == companyId).ToListAsync();
        }
    }
}
