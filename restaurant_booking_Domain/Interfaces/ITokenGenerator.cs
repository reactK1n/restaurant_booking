using restaurant_booking_Domain.Entities;
using System.Threading.Tasks;

namespace restaurant_booking_Domain.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUsers user);
    }
}
