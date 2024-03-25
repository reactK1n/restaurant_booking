using MediatR;
using restaurant_booking_Application.Common;

namespace restaurant_booking_Application.AuthCQRS.Commands
{
    public class RegisterCommand : IRequest<Response<bool>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
