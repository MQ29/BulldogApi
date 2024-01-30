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
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task Create(string userId, Guid serviceId, string ServiceName, Guid employeeId, DateTime startDate, DateTime finishDate)
        {
            var reservation = new Reservation(userId, serviceId, ServiceName, employeeId, startDate, finishDate);
            await _reservationRepository.AddAsync(reservation);
        }

        public async Task<IList<ReservationDto>> GetForEmployeeId(Guid employeeId)
        {
            var reservations = await _reservationRepository.GetForEmployeeId(employeeId);
            if (reservations is null)
            {
                throw new Exception($"No reservations for employee with id {employeeId}");
            }
            var mappedReservations = _mapper.Map<IList<ReservationDto>>(reservations);

            return mappedReservations;
        }
    }
}
