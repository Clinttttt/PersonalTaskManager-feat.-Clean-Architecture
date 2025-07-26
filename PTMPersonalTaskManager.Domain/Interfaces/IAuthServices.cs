using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Domain.Interfaces
{
   public interface IAuthServices
    {

     Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto request);
     Task<TaskProperties?> RegisterAsync(UserDto request);
     Task<TokenResponseDto?> HandleLogin(UserDto request);
     Task<bool> LogoutAsync(Guid id);
    }
}
