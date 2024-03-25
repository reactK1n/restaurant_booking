using AutoMapper;
using restaurant_booking_Application.AuthCQRS.Commands;
using restaurant_booking_Application.MealCQRS.Commands;
using restaurant_booking_Application.MealCQRS.Responses;
using restaurant_booking_Application.ReservationCQRS.Commands;
using restaurant_booking_Domain.Entities;

namespace restaurant_booking_Application.MapperClass
{
    public class AutoMapperInitialize : Profile
    {
        public AutoMapperInitialize()
        {
            //Meal Mapping
            CreateMap<Meal, GetMealDtos>()
                .ForMember(x => x.Price, y => y.MapFrom(x=>x.MealPrices))
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id));

            CreateMap<MealPrice, GetPrice>()
                .ForMember(x => x.PriceDetail, y => y.MapFrom(x => x.PriceDetail))
                .ForMember(x => x.Table, y => y.MapFrom(x => x.TypeOfTable)).ReverseMap();

            CreateMap<AddMealCommand, Meal>()
                .ForMember(x => x.MealPrices, y => y.MapFrom(x => x.Prices)).ReverseMap();

            CreateMap<AppUsers, RegisterCommand>()
                .ForMember(x => x.Password, y => y.MapFrom(x => x.PasswordHash))
                .ForMember(x => x.Email, y => y.MapFrom(x => x.UserName))
                .ForMember(x => x.Address, y => y.MapFrom(x => x.Customer.Address)).ReverseMap();

            CreateMap<ReservationCommand, Reservation>();
        }
    }
}
