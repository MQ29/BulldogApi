using Bulldog.Infrastructure.Services.DTO;

namespace BulldogUI.Client.Services
{
    public interface IApiEmployeeService    
    {
        Task<IList<EmployeeDto>> GetAllEmployees();
    }
}
