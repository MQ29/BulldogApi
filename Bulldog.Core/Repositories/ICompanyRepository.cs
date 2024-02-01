using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Repositories
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);
        Task<Company> GetAsync(Guid companyId);
        Task Update(Company company);
        Task<Company> GetByUserIdAsync(string userId);
    }
}
