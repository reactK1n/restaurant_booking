using MediatR;
using restaurant_booking_Application.Common;

namespace restaurant_booking_Application.AuthCQRS.Commands
{
    public class LoginCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
