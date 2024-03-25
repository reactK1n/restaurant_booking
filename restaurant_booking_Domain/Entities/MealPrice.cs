using restaurant_booking_Domain.Common;
using restaurant_booking_Domain.Enum;

namespace restaurant_booking_Domain.Entities
{
    public class MealPrice : BaseEntity
    {
        public Meal Meal { get; set; }
        public double PriceDetail { get; set; }
        public TableType TypeOfTable { get; set; }
    }
}
