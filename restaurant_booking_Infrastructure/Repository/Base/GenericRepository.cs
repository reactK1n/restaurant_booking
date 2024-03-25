using Microsoft.EntityFrameworkCore;
using restaurant_booking_Domain.IRepository.Base;
using restaurant_booking_Infrastructure.Contexts;
using System;
using System.Threading.Tasks;

namespace restaurant_booking_Infrastructure.Repository.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RbaContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(RbaContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
