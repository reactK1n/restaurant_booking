using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using restaurant_booking_Domain.Common;
using restaurant_booking_Domain.Entities;
using restaurant_booking_Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_booking_Infrastructure.SeederClass
{
    public class RbaSeeder
    {
        public static async Task SeedData(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            await Seeder(
                (UserManager<AppUsers>)serviceScope.ServiceProvider.GetService(typeof(UserManager<AppUsers>)),
                serviceScope.ServiceProvider.GetService<RbaContext>(),
                (RoleManager<IdentityRole>)serviceScope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>))
                );
        }
        private async static Task Seeder(UserManager<AppUsers> userManager, RbaContext context, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var baseDir = Directory.GetCurrentDirectory();

                await context.Database.EnsureCreatedAsync();
                if (!context.Users.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = UserRole.Admin.ToString() });
                    await roleManager.CreateAsync(new IdentityRole { Name = UserRole.Customer.ToString() });

                    var userList = new List<AppUsers>
                    {
                        new AppUsers
                        {
                            FirstName = "Samuel",
                            LastName = "Adeosun",
                            Email = "samuel@gmail.com",
                            UserName = "Allos",
                            PhoneNumber = "08165434179",
                            PasswordHash = "Password@123",
                            EmailConfirmed = true,
                            Avatar = "http://placehold.it/32x32",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new AppUsers
                        {
                            FirstName = "Gideon",
                            LastName = "Faive",
                            Email = "gideon@gmail.com",
                            UserName = "faive",
                            PhoneNumber = "08143547856",
                            PasswordHash = "Password@123",
                            EmailConfirmed = true,
                            Avatar = "http://placehold.it/32x32",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new AppUsers
                        {
                            FirstName = "Ombu",
                            LastName = "Ayebakuro",
                            Email = "kuro@gmail.com",
                            UserName = "iceboss",
                            PhoneNumber = "08186957401",
                            PasswordHash = "Password@123",
                            EmailConfirmed = true,
                            Avatar = "http://placehold.it/32x32",
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        }
                    };

                    foreach (var user in userList)
                    {
                        await userManager.CreateAsync(user, user.PasswordHash);
                        if (user == userList[0])
                        {
                            await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
                        }
                        else
                            await userManager.AddToRoleAsync(user, UserRole.Customer.ToString());
                    }
                }

                if (!context.Meals.Any())
                {
                    var path = File.ReadAllText(FilePath(baseDir, "Json/Meal.json"));

                    var meal = JsonConvert.DeserializeObject<List<Meal>>(path);
                    await context.Meals.AddRangeAsync(meal);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}
