using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Services;
using System.Security.Claims;

namespace PTMPersonalTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterAsync(UserDto register)
        {
            var request = await authServices.RegisterAsync(register);
            if(request is null)
            {
                return BadRequest("User Already Exists");
               
            }
            return Ok(request);
        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<User?>> LoginAsync(UserDto request)
        {
            var user = await authServices.HandleLogin(request);
            if(user is null)
            {
                return BadRequest("Cannot Find User");
            }
            return Ok(user);
        }
        [HttpPost("Request-Token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshTokenAsync(RefreshTokenDto request)
        {
            var user = await authServices.RefreshTokenAsync(request);
            if (user is null || user.RefreshToken is null || request.RefreshToken is null)
            {
                return BadRequest("Unauthorized User");
            }
            return Ok(user);
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            var find = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (find is null || !Guid.TryParse(find.Value, out var Results))
            {
                return BadRequest("Something went wrong");
                    
            }
            var user = await authServices.LogoutAsync(Results);
            if (!user)
            {
                return BadRequest("User not exisits");
            
            }
            return Ok(new { message = "Logout Successfully"});
        }



    }
}
