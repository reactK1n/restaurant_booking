using Microsoft.Extensions.DependencyInjection;

namespace restaurant_booking_api
{
    public static class AuthMiddleware
    {
        public static void AddPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                configure.AddPolicy(Policies.Customer, Policies.CustomerPolicy());
            });
        }
    }
}
