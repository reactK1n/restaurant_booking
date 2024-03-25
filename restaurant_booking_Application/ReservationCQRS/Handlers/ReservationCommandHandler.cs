using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using restaurant_booking_Application.Common;
using restaurant_booking_Application.ReservationCQRS.Commands;
using restaurant_booking_Domain.Entities;
using restaurant_booking_Domain.IRepository.Base;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace restaurant_booking_Application.ReservationCQRS.Handlers
{
    public class ReservationCommandHandler : IRequestHandler<ReservationCommand, Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Reservation> _unit;
        private readonly UserManager<AppUsers> _userManager;

        public ReservationCommandHandler(IMapper mapper, 
            IGenericRepository<Reservation> unit,
            UserManager<AppUsers> userManager)
        {
            _mapper = mapper;
            _unit = unit;
            _userManager = userManager;
        }
        public async Task<Response<bool>> Handle(ReservationCommand request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.Include(x => x.Customer).FirstOrDefault(x => x.Id == request.LoggedUserId);
            var reservation = _mapper.Map<Reservation>(request);
            reservation.Customer = user.Customer;
            await _unit.InsertAsync(reservation);
            await _unit.SaveAsync();
            return Response<bool>.Success("Reservation made successfully", true, StatusCodes.Status200OK);
        }
    }
}
