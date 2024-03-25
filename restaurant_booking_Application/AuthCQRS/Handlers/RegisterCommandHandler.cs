using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using restaurant_booking_Application.AuthCQRS.Commands;
using restaurant_booking_Application.Common;
using restaurant_booking_Domain.Common;
using restaurant_booking_Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace restaurant_booking_Application.AuthCQRS.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUsers> _userManager;

        public RegisterCommandHandler(IMapper mapper, 
            UserManager<AppUsers> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Response<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUsers>(request);
            user.CreatedAt = DateTime.Now;
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole.Customer.ToString());

                return Response<bool>.Success("User added successfully", true, StatusCodes.Status200OK);
            }
            return Response<bool>.Fail(ErrorMessage.GetErrors(result), StatusCodes.Status400BadRequest);
        }
    }
}
