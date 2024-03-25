using System.Collections.Generic;

namespace restaurant_booking_Application.MealCQRS.Responses
{
    public class GetMealDtos
    {
        public string Id { get; set; }
        public string MealName { get; set; }
        public string ThumbNail { get; set; }
        public ICollection<GetPrice> Price { get; set; }
    }

    public class GetPrice
    {
        public double PriceDetail { get; set; }
        public string Table { get; set; }
    }
}
