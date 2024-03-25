using FluentValidation;
using restaurant_booking_Application.ReservationCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_booking_Application.Common.Validators
{
    public class ReservationValidator : AbstractValidator<ReservationCommand>
    {
        public ReservationValidator()
        {
            RuleFor(reservation => reservation.FullName).Name();
            RuleFor(reservation => reservation.HourBooked)
                .NotNull().WithMessage("Hour booked cannot be null")
                .NotEmpty().WithMessage("Hour booked cannot be empty");
            RuleFor(reservation => reservation.NoOfPeople)
                .NotNull().WithMessage("Hour booked cannot be null")
                .GreaterThan(0).WithMessage("No of People must be greater than zero");
            RuleFor(reservation => reservation.PhoneNumber).PhoneNumber();
            RuleFor(reservation => reservation.TypeOfTable)
                .NotNull().WithMessage("Hour booked cannot be null");
        }
    }
}
