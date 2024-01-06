using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.Reservations;
using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulldog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        public async Task Post([FromBody] CreateReservation request)
        {
            await _reservationService.Create(request.Service, request.Employee, request.Date);
        }
    }
}
