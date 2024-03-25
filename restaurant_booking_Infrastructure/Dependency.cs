using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using restaurant_booking_Domain.Common;
using restaurant_booking_Domain.Entities;
using restaurant_booking_Domain.Interfaces;
using restaurant_booking_Domain.IRepository;
using restaurant_booking_Domain.IRepository.Base;
using restaurant_booking_Infrastructure.Contexts;
using restaurant_booking_Infrastructure.Implementation;
using restaurant_booking_Infrastructure.Repository;
using restaurant_booking_Infrastructure.Repository.Base;
using System;
using System.Text;

namespace restaurant_booking_Infrastructure
{
    public static class Dependency
    {
        public static void DataDependency(this IServiceCollection service, IConfiguration Configuration)
        {
            service.AddScoped<IMealRepository, MealRepository>();

            service.AddScoped<ITokenGenerator, TokenGenerator>();

            service.AddScoped<IGenericRepository<Reservation>, GenericRepository<Reservation>>();

            new IdentityBuilder(
                service.AddIdentity<AppUsers, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                }).UserType, typeof(IdentityRole), service)
                .AddEntityFrameworkStores<RbaContext>()
                .AddDefaultTokenProviders();

            var emailConfiguration = new MailSettings
            {
                Mail = Configuration["MailSettings:Mail"],
                DisplayName = Configuration["MailSettings:DisplayName"],
                Password = Configuration["MailSettings:Password"],
                Host = Configuration["MailSettings:Host"],
                Port = Convert.ToInt32(Configuration["MailSettings:Port"])
            };

            service.AddSingleton(emailConfiguration);

            var tokenProvider = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = Configuration["JwtSettings:Issuer"],
                ValidAudience = Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding
                        .UTF8.GetBytes(Configuration["JwtSettings:SecretKey"])),
                ClockSkew = TimeSpan.Zero
            };

            service.AddSingleton(tokenProvider);

            service.AddAuthentication(option => 
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = tokenProvider;
                });
        }

        public static IServiceCollection AddCloudinary(this IServiceCollection services,
            Account account, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.Add(new ServiceDescriptor(typeof(Cloudinary), c => new Cloudinary(account), lifetime));
            return services;
        }

        public static Account GetAccount(IConfiguration Configuration)
        {
            Account account = new(
                                Configuration["ImageUploadSettings:CloudName"],
                                Configuration["ImageUploadSettings:ApiKey"],
                                Configuration["ImageUploadSettings:ApiSecret"]);
            return account;
        }
    }
}
