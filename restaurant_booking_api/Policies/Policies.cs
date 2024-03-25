using Microsoft.AspNetCore.Authorization;

namespace restaurant_booking_api
{
    public static class Policies
    {
        /// <summary>
        /// Admin policy
        /// </summary>
        public const string Admin = "Admin";

        public const string Customer = "Customer";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy CustomerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Customer).Build();
        }
    }
}
