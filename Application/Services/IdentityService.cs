using Api.Domain;
using Application.Configurations;
using Application.Repositories;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtSettings jwtSettings;
        private readonly IIdentityRepository identityRepository;

        public IdentityService(
            //UserManager<IdentityUser> userManager,
            //JwtSettings jwtSettings,
            IIdentityRepository identityRepository)
        {
            //this.userManager = userManager;
            //this.jwtSettings = jwtSettings;
            this.identityRepository = identityRepository;
        }

        public void Login()
        {

        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new string[] { "Email already exists." }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email,
            };

            var createdUser = await userManager.CreateAsync(newUser, password);

            if (createdUser.Succeeded == false)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);

            return new AuthenticationResult
            {
                Token = tokenHandler.WriteToken(token),
                Success = true
            };
        }

    }
}
