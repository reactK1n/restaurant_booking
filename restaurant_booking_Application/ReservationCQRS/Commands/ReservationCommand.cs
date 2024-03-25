using MediatR;
using restaurant_booking_Application.Common;
using restaurant_booking_Domain.Enum;
using System;

namespace restaurant_booking_Application.ReservationCQRS.Commands
{
    public class ReservationCommand : IRequest<Response<bool>>
    {
        public string LoggedUserId { get; set; }
        public DateTime DateBooked { get; set; }
        public string FullName { get; set; }
        public string HourBooked { get; set; }
        public string PhoneNumber { get; set; }
        public int NoOfPeople { get; set; }
        public TableType TypeOfTable { get; set; }
    }
}
