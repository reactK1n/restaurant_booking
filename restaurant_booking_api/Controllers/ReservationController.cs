using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restaurant_booking_Application.Common;
using restaurant_booking_Application.ReservationCQRS.Commands;
using System.Security.Claims;
using System.Threading.Tasks;

namespace restaurant_booking_api.Controllers
{
    [Authorize(Policy = Policies.Customer)]
    public class ReservationController : ApiController
    {
        [HttpPost("book-reservation")]
        public async Task<ActionResult<Response<bool>>> Reservation(ReservationCommand command)
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            command.LoggedUserId = userId;
            return await Mediator.Send(command);
        }
    }
}
