using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Repositories
{
    public interface IAvailableHourRepository
    {
        Task<bool> SaveChangesAsync();
        Task AddAsync(AvailableHour hour);
        Task EmptyTableAsync();

    }
}
