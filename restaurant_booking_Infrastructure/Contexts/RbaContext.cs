using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurant_booking_Domain.Common;
using restaurant_booking_Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace restaurant_booking_Infrastructure.Contexts
{
    public class RbaContext : IdentityDbContext<AppUsers>
    {
        public RbaContext(DbContextOptions<RbaContext> getData) : base(getData)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Reviews> Review { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealPrice> MealPrices { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        item.Entity.Id = Guid.NewGuid().ToString();
                        item.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
