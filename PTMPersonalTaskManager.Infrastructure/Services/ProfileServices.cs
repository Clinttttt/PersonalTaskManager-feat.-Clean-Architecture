using Mapster;
using Microsoft.EntityFrameworkCore;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Data;
using PTMPersonalTaskManager.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = PTMPersonalTaskManager.Domain.Entities.Profile;

namespace PTMPersonalTaskManager.Infrastructure.Services
{
    public class ProfileServices(ApplicationDbContext context) : IProfileServices
    {

        public async Task<ProfileDto> AddProfile(Profile profile)
        {
       
            var profiles = new Profile
            {
                Id = profile.Id,
                FullName = profile.FullName,
               PhoneNumber = profile.PhoneNumber,
                ProfilePicture = profile.ProfilePicture
            };
            var filter = profiles.Adapt<ProfileDto>();
            context.profile.Add(profiles);
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<IEnumerable<Profile>?> DisplayProfile(Guid id)
        {

            return await context.profile.Where(u => u.Id == id).ToListAsync();
        }
  


        










    }
}
