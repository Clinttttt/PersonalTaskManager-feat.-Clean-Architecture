using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.DTOs.DetailsDto;
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
        [Authorize]
        [HttpGet("ListTask")]
        public async Task<ActionResult<DetailsDto?>> ToList()
        {
            var findGuid = User.FindFirst(ClaimTypes.NameIdentifier);
            if(findGuid == null)
            {
                return BadRequest("Login First");
            }
            var ConvertGuid = Guid.Parse(findGuid.Value);
            var List = await Taskmanager.ListData(ConvertGuid);
            if (List is null)
            {
                return BadRequest("Nothing to List");
            }
            var filter = List.Adapt<List<DetailsDto>>();
           
            return filter is not null ? Ok(filter) : BadRequest("Nothing to List");
        }

        //working
        [Authorize]
        [HttpPost("CreateData")]
        public async Task<ActionResult<DetailsDto?>> CreateData([FromBody] CreateTaskDto create)

        {
            var findGuid = User.FindFirst(ClaimTypes.NameIdentifier);
            if(findGuid is null)
            {
                return BadRequest("Login First");
            }

            var ConvertGuid = Guid.Parse(findGuid.Value);           
            var filter = create.Adapt<TaskProperties>();
            filter.UserId = ConvertGuid;
            var createdata = await Taskmanager.CreateData(filter);
            if (createdata is null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(createdata);
        }


        //working
        [Authorize]
        [HttpGet("GetData/{Id}")]
        public async Task<ActionResult<DetailsDto?>> GetData(Guid Id)
        {
            var FindGuid = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindGuid is null)
            {
                return BadRequest("Login First");
            }
            var ConvertGuid = Guid.Parse(FindGuid.Value);
            var find = await Taskmanager.TaskReadData(Id);
            if(find is null || find.UserId != ConvertGuid)
            {
                return BadRequest("Data not found");
            }
            var filter = find.Adapt<DetailsDto>();
            return filter;
        }
        //working
        [Authorize]
        [HttpPatch("UpdateData")]
        public async Task<ActionResult<DetailsDto>> UpdateData(UpdateTaskDto update)
        {
            var FindGuid = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindGuid is null)
            {
                return BadRequest("Login First");
            }
            var ConvertGuid = Guid.Parse(FindGuid.Value);

            var updatedata = await Taskmanager.UpdateData(update);
            var filters = updatedata.Adapt<DetailsDto>();

            return filters is not null && filters.UserId == ConvertGuid? Ok(filters) : BadRequest("Something went wrong");
        }
        //working
        [Authorize]
        [HttpGet("DeleteData{Id}")]
        public async Task<ActionResult<DetailsDto>> DeleteData(Guid Id)
        {
            var FindGuid = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindGuid is null)
            {
                return BadRequest("Login First");
            }
            var ConvertGuid = Guid.Parse(FindGuid.Value);
            var deletedata = await Taskmanager.DeleteData(Id);
            return deletedata is not null && deletedata.UserId == ConvertGuid ? Ok(deletedata) : BadRequest("No data");
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

