using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class InMemoryReservationRepository : IReservationRepository
    {
        private static ISet<Reservation> _reservations = new HashSet<Reservation>();
        public async Task AddAsync(Reservation reservation)
        {
            _reservations.Add(reservation);
            await Task.CompletedTask;
        }
    }
}
