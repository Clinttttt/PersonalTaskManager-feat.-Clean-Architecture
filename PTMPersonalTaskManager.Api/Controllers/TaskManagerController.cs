using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Migrations;
using System.Security.Claims;
using Profile = PTMPersonalTaskManager.Domain.Entities.Profile;

namespace PTMPersonalTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController(ITaskmanager Taskmanager, IProfileServices profileServices) : ControllerBase
    {
        //working
        [HttpGet("ListTask")]
        public async Task<ActionResult<TaskProperties?>> ToList()
        {
            var List = await Taskmanager.ListData();
            var filter = List.Adapt<List<DetailsDto>>();
            return filter is not null ? Ok(filter) : BadRequest("Nothing to List");
        }

        //working
        [HttpPost("CreateData")]
        public async Task<ActionResult<TaskProperties?>> CreateData([FromBody] DetailsDto create)
        {
            var filter = create.Adapt<TaskProperties>();
            var createdata = await Taskmanager.CreateData(filter);
            if (createdata is null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(createdata);
        }


        //working
        [HttpGet("GetData/{id}")]
        public async Task<ActionResult<TaskProperties?>> GetData(Guid id)
        {
            var find = await Taskmanager.TaskReadData(id);
            var filter = find.Adapt<DetailsDto>();
            return filter is not null ? Ok(filter) : BadRequest("Data not found");
        }
        //working
        [HttpPatch("UpdateData")]
        public async Task<ActionResult<TaskProperties>> UpdateData(DetailsDto update)
        {

            var updatedata = await Taskmanager.UpdateData(update);
            var filters = updatedata.Adapt<DetailsDto>();

            return filters is not null ? Ok(filters) : BadRequest("Something went wrong");
        }
        //working
        [HttpGet("DeleteData")]
        public async Task<ActionResult<Taskproperties>> DeleteData(Guid id)
        {
            var deletedata = await Taskmanager.DeleteData(id);
            return deletedata is not null ? Ok(deletedata) : BadRequest("No data");
        }
        [Authorize]
        [HttpPost("AddProfile")]
        public async Task<ActionResult<Profile>> AddProfileAsync(ProfileDto profile)
        {
            var find = User.FindFirst(ClaimTypes.NameIdentifier);
            if (find is null)
            {
                return BadRequest("Login First");
            }
            var userId = Guid.Parse(find.Value);
            var filter = profile.Adapt<Profile>();
            filter.Id = userId;
           var CheckProfile = await profileServices.DisplayProfile(userId);
            if(!CheckProfile.IsNullOrEmpty())
            {
                return BadRequest("Already Have an Account");
            }
            var request = await profileServices.AddProfile(filter);

            return Ok(request);
        }
        [Authorize]
        [HttpGet("DisplayProfile")]
        public async Task<ActionResult<Profile>> DisplayProfileAsync()
        {
            var find = User.FindFirst(ClaimTypes.NameIdentifier);
            if (find is null)
            {
                return BadRequest("Login First");
            }
            var parse = Guid.Parse(find.Value);
            var request = await profileServices.DisplayProfile(parse);
            if(request is null)
            {
                return BadRequest("no Profile");
            }
           return Ok(request);
        }


    }

}

