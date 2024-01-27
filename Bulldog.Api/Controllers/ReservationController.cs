using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.Reservations;
using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulldog.Api.Controllers
{
    [Authorize]
    [Route("Reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReservation request)
        {

            await _reservationService.Create(request.UserId, request.ServiceId, request.EmployeeId, request.Date);
            return Ok();
        }

    }
}
