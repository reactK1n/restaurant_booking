using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using restaurant_booking_Application.Common;
using restaurant_booking_Application.MealCQRS.Responses;
using restaurant_booking_Application.MealCQRS.Queries;
using restaurant_booking_Domain.IRepository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace restaurant_booking_Application.Meals.Handlers
{
    public class GetMealsCommandQueryHandler : IRequestHandler<GetMealsCommandQuery,  Response<IEnumerable<GetMealDtos>>>
    {
        private readonly IMapper _mapper;
        private readonly IMealRepository _mealRepository;
        public GetMealsCommandQueryHandler(IMapper mapper,
            IMealRepository mealRepository)
        {
            _mapper = mapper;
            _mealRepository = mealRepository;
        }

        public async Task<Response<IEnumerable<GetMealDtos>>> Handle(GetMealsCommandQuery request, CancellationToken cancellationToken)
        {
            var meals = await _mealRepository.GetAllMeal();
            var mapMeal = _mapper.Map<List<GetMealDtos>>(meals);
            return Response<IEnumerable<GetMealDtos>>.Success("Meal displayed", mapMeal);
        }
    }
}
