using Azure.Core;
using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public interface ICompanyService
    {
        Task Create(Company company);
        Task Update(Guid companyId, Address address, string name, string phoneNumber);
        Task<CompanyDto> GetCompanyAsync(Guid companyId);
        Task<CompanyDto> GetByUserIdAsync(string userId);

    }
}
