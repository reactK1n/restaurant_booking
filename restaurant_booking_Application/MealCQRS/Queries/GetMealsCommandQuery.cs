using MediatR;
using restaurant_booking_Application.Common;
using restaurant_booking_Application.MealCQRS.Responses;
using System.Collections.Generic;

namespace restaurant_booking_Application.MealCQRS.Queries
{
    public class GetMealsCommandQuery : IRequest<Response<IEnumerable<GetMealDtos>>>
    {
    }
}
