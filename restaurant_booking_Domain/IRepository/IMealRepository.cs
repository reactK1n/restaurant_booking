using restaurant_booking_Domain.Entities;
using restaurant_booking_Domain.IRepository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace restaurant_booking_Domain.IRepository
{
    public interface IMealRepository : IGenericRepository<Meal>
    {
        Task<IEnumerable<Meal>> GetAllMeal();
    }
}
