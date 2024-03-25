using restaurant_booking_Domain.Common;

namespace restaurant_booking_Domain.Entities
{
    public class Reviews : BaseEntity
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public Customer Customer { get; set; }
    }
}
