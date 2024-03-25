using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using restaurant_booking_Domain.Entities;
using restaurant_booking_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace restaurant_booking_Infrastructure.Implementation
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration Configuration;
        private readonly UserManager<AppUsers> _userManager;

        public TokenGenerator(UserManager<AppUsers> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            Configuration = configuration;
        }

        public async Task<string> GenerateToken(AppUsers user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"]));

            var getToken = new JwtSecurityToken
                (audience: Configuration["JWTSettings:Audience"],
                issuer: Configuration["JWTSettings:Issuer"],
                claims: authClaims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(getToken);
        }
    }
}
