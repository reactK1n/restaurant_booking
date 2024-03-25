using MediatR;
using restaurant_booking_Application.Common;
using restaurant_booking_Application.MealCQRS.Responses;
using System.Collections.Generic;

namespace restaurant_booking_Application.MealCQRS.Commands
{
    public class AddMealCommand : IRequest<Response<bool>>
    {
        public string MealName { get; set; }
        public string ThumbNail { get; set; }
        public List<GetPrice> Prices { get; set; }
    }
}
