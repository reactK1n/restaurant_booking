using restaurant_booking_Domain.Common;
using System.Collections.Generic;

namespace restaurant_booking_Domain.Entities
{
    public class Meal : BaseEntity
    {
        public string MealName { get; set; }
        public string ThumbNail { get; set; }
        public ICollection<MealPrice> MealPrices { get; set; }
    }
}
