using Microsoft.EntityFrameworkCore;
using restaurant_booking_Domain.Entities;
using restaurant_booking_Domain.IRepository;
using restaurant_booking_Infrastructure.Contexts;
using restaurant_booking_Infrastructure.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_booking_Infrastructure.Repository
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(RbaContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Meal>();

        }
        private readonly RbaContext _context;
        private readonly DbSet<Meal> _dbSet;

        public async Task<IEnumerable<Meal>> GetAllMeal()
        {
            var a =  await _dbSet.Include(x => x.MealPrices).Select(x => new Meal()
            {
                MealName = x.MealName,
                ThumbNail = x.ThumbNail,
                MealPrices = x.MealPrices,
                Id = x.Id
            }).ToListAsync();

            return a;
        }
    }
}
