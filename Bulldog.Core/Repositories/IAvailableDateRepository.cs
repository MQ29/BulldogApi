using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Repositories
{
    public interface IAvailableDateRepository
    {
        Task AddAsync(AvailableDate availableDate);
        Task<IList<AvailableDate>> GetAsync(Guid Id);
    }
}
