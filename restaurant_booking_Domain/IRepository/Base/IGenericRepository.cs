using System.Threading.Tasks;

namespace restaurant_booking_Domain.IRepository.Base
{
    public interface IGenericRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
