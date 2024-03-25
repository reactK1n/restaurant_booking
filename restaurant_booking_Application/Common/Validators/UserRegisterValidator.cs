using FluentValidation;
using restaurant_booking_Application.AuthCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_booking_Application.Common.Validators
{
    public class UserRegisterValidator : AbstractValidator<RegisterCommand>
    {
        public UserRegisterValidator()
        {
            RuleFor(user => user.FirstName).Name();

            RuleFor(user => user.LastName).Name();

            RuleFor(user => user.Email).EmailAddress();

            RuleFor(user => user.Password).Password();

            RuleFor(user => user.Address).Address();
        }
    }
}
