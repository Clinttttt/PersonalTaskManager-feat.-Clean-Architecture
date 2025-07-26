using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Domain.Interfaces
{
    public interface IProfileServices
    {
       Task<ProfileDto> AddProfile(Profile profile);
       Task<IEnumerable<Profile>?> DisplayProfile(Guid id);
    }
}
