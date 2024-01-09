using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class EFReservationRepository : IReservationRepository
    {
        private readonly BulldogDbContext _dbContext;

        public EFReservationRepository(BulldogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Reservation reservation)
        {
            await _dbContext.Reservations.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
