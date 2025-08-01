using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure.Services
{
   public class AuthServices(ApplicationDbContext context, IConfiguration configuration) : IAuthServices
    {

       public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto request)
        {
            var user = await ValidateRefreshToken(request.UserId, request.RefreshToken);
            if (user is null)
            {
                return null;
            }
            return await CreateTokenResponse(user);
        }

        public async Task<User?> RegisterAsync(UserDto request)
        {
            if (await context.user.AnyAsync(u => u.Username == request.UserName))
            {
                return null;
            }
            var user = new User();
            var hashPassword = new PasswordHasher<User>()
                 .HashPassword(user, request.Password);
            user.Username = request.UserName;
            user.Password = hashPassword;
            context.user.Add(user);
            await context.SaveChangesAsync();
            return user;

        }
        public async Task<TokenResponseDto?> HandleLogin(UserDto request)
        {
            var user = await context.user.FirstOrDefaultAsync(u => u.Username == request.UserName);
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return await CreateTokenResponse(user);
        }

        //1
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.Name, user.Username),
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
           
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var TokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
               claims: claims,
               expires: DateTime.UtcNow.AddDays(1),
               signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(TokenDescriptor);
        }
       
        public string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);
        }
        //3
        public async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var RefreshToken = GenerateRefreshToken();
            user.RefreshToken = RefreshToken;
            user.ExpiredRefreshToken = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return RefreshToken;
        }
        //4
        public async Task<User?> ValidateRefreshToken( Guid UserId, string RefreshToken)
        {
            var user = await context.user.FindAsync(UserId);
            if(user is null || user.RefreshToken != RefreshToken || user.ExpiredRefreshToken <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }     
        //5
       public async Task<TokenResponseDto> CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)

            };
        }
        public async Task<bool> LogoutAsync(Guid id)
        {
            var user = await context.user.FindAsync(id);
            if(user is null)
            {
                return false;
            }
            user.RefreshToken = null;
            user.ExpiredRefreshToken = DateTime.UtcNow.AddDays(-1);
            await context.SaveChangesAsync();
            return true;
        }
          
  








        }

    }

