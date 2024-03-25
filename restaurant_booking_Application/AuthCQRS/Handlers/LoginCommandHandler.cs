using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using restaurant_booking_Application.AuthCQRS.Commands;
using restaurant_booking_Application.Common;
using restaurant_booking_Domain.Entities;
using restaurant_booking_Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace restaurant_booking_Application.AuthCQRS.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<string>>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginCommandHandler(UserManager<AppUsers> userManager,
            ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Response<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user !=null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var token = await _tokenGenerator.GenerateToken(user);

                return Response<string>.Success("Login Successful", token, StatusCodes.Status200OK);
            }
            return Response<string>.Fail("Invalid Login Credentials", StatusCodes.Status404NotFound);
        }
    }
}
