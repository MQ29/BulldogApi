using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IMapper mapper, IReservationRepository reservationRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }
        public async Task Create(Guid serviceId, Guid employeeId, DateTime date)
        {
            // Tutaj możesz wykorzystać identyfikatory do utworzenia obiektu Reservation.

            // Następnie tworzysz rezerwację:
            var reservation = new Reservation(serviceId, employeeId, date);

            // Dodaj do repozytorium rezerwacji:
            await _reservationRepository.AddAsync(reservation);
        }
    }
}
