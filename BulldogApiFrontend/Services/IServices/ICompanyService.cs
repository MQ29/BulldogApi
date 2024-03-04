using Bulldog.Infrastructure.Services.DTO;

namespace BulldogApiFrontend.Services.IServices
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompanyAsync(Guid companyId);
    }
}
