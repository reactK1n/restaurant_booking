using Microsoft.AspNetCore.Mvc;
using restaurant_booking_Application.AuthCQRS.Commands;
using restaurant_booking_Application.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace restaurant_booking_api.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<Response<string>>> Login([FromBody] LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Response<bool>>> Register([FromBody] RegisterCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
