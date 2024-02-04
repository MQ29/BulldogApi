using Bulldog.Infrastructure.Services.DTO;

namespace Shared.IServices
{
    public interface IApiEmployeeService
    {
        Task<IList<EmployeeDto>> GetAllEmployees();
    }
}
