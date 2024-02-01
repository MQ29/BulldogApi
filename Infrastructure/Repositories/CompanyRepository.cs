using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly BulldogDbContext _dbContext;
        public CompanyRepository(BulldogDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task AddAsync(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Company> GetAsync(Guid companyId)
        {
            return await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<Company> GetByUserIdAsync(string userId)
        {
            return await _dbContext.Companies.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task Update(Company company)
        {
            var existingCompany = _dbContext.Companies.Find(company.Id);
            if (existingCompany is null)
            {
                throw new Exception($"Error: No company found with Id: {existingCompany.Id}");
            }
            existingCompany.SetAddress(company.Address);
            existingCompany.SetName(company.Name);
            existingCompany.SetPhoneNumber(company.PhoneNumber);
            await _dbContext.SaveChangesAsync();
        }
    }
}
