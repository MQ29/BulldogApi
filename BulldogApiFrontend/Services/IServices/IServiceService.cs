using Bulldog.Infrastructure.Services.DTO;

namespace BulldogApiFrontend.Services.IServices
{
    public interface IServiceService
    {
        Task<IList<ServiceDto>> GetServicesByCompanyId(Guid companyId);
    }
}
