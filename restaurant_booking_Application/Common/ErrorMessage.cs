using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace restaurant_booking_Application.Common
{
    public class ErrorMessage
    {
        public static string GetErrors(IdentityResult result)
        {
            return result.Errors.Aggregate(string.Empty, (current, err) => current + err.Description + "\n");
        }
    }
}
