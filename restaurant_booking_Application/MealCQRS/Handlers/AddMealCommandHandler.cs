using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using restaurant_booking_Application.Common;
using restaurant_booking_Application.MealCQRS.Commands;
using restaurant_booking_Domain.Entities;
using restaurant_booking_Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace restaurant_booking_Application.MealCQRS.Handlers
{
    public class AddMealCommandHandler : IRequestHandler<AddMealCommand, Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IMealRepository _mealRepository;
        public AddMealCommandHandler(IMapper mapper,
            IMealRepository mealRepository)
        {
            _mapper = mapper;
            _mealRepository = mealRepository;
        }
        public async Task<Response<bool>> Handle(AddMealCommand request, CancellationToken cancellationToken)
        {
            var addMeal = _mapper.Map<Meal>(request);
            await _mealRepository.InsertAsync(addMeal);
            await _mealRepository.SaveAsync();
            return Response<bool>.Success("New Meal Added Successfully", true, StatusCodes.Status200OK);
        }
    }
}
