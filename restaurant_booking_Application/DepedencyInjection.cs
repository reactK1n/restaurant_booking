using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using restaurant_booking_Application.AuthCQRS.Commands;
using restaurant_booking_Application.Common.Validators;
using restaurant_booking_Application.MapperClass;
using restaurant_booking_Application.ReservationCQRS.Commands;
using System.Reflection;

namespace restaurant_booking_Application
{
    public static class DepedencyInjection
    {
        public static void AddMediatRInjection(this IServiceCollection service)
        {
            service.AddMediatR(Assembly.GetExecutingAssembly());
            service.AddAutoMapper(typeof(AutoMapperInitialize));

            service.AddFluentValidation(fv => {
                fv.DisableDataAnnotationsValidation = true;
                fv.ImplicitlyValidateChildProperties = true;
            });

            //Register fluent validation
            service.AddTransient<IValidator<RegisterCommand>, UserRegisterValidator>();
            service.AddTransient<IValidator<ReservationCommand>, ReservationValidator>();
        }
    }
}
