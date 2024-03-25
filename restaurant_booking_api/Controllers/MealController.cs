using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_booking_Application.Common;
using restaurant_booking_Application.MealCQRS.Commands;
using restaurant_booking_Application.MealCQRS.Queries;
using restaurant_booking_Application.MealCQRS.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace restaurant_booking_api.Controllers
{
    public class MealController : ApiController
    {
        /*[HttpGet]
        public async Task<ActionResult<List<GetMealDtos>>> GetMeals()
        {
            try
            {
                return await Mediator.Send(new GetMealsCommandQuery());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }*/

        [HttpGet("get-all-meals")]

        public async Task<ActionResult<Response<IEnumerable<GetMealDtos>>>> GetMeals()
        {
            return await Mediator.Send(new GetMealsCommandQuery());
        }

        [HttpPost("add-meal")]
        [Authorize(Policy = Policies.Admin)]
        public async Task<ActionResult<Response<bool>>> AddMeal(AddMealCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
