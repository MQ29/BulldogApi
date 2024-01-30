using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public interface IReservationService
    {
        Task Create(string userId, Guid serviceId, string serviceName, Guid employeeId, DateTime startDate,
            DateTime finishDate);
        Task<IList<ReservationDto>> GetForEmployeeId(Guid employeeId);
    }
}
