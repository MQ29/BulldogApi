using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.Reservations;
using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

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

            await _reservationService.Create(request.UserId, request.ServiceId, request.ServiceName, request.EmployeeId
                    , request.StartDate, request.FinishDate);
            return Ok();
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> Get(Guid employeeId)
        {
            var reservations = await _reservationService.GetForEmployeeId(employeeId);
            return Ok(reservations);
        }

    }
}
