using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace restaurant_booking_Domain.Entities
{
    public class Customer
    {
        [Key]
        public string AppUsersId { get; set; }
        public AppUsers AppUsers { get; set; }
        public string Address { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
