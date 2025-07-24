using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Migrations;

namespace PTMPersonalTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController(ITaskmanager Taskmanager) : ControllerBase
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



        [HttpGet("GetData/{id}")]
        public async Task<ActionResult<TaskProperties?>> GetData(Guid id)
        {
            var find = await Taskmanager.TaskReadData(id);
            var filter = find.Adapt<DetailsDto>();
            return filter is not null ? Ok(filter) : BadRequest("Data not found");
        }
        [HttpPatch("UpdateData")]
        public async Task<ActionResult<TaskProperties>> UpdateData(DetailsDto update)
        {
            
            var updatedata = await Taskmanager.UpdateData(update);
            var filters = updatedata.Adapt<DetailsDto>();
            
            return filters is not null ? Ok(filters) : BadRequest("Something went wrong");
        }
        [HttpGet("DeleteData")]
        public async Task<ActionResult<Taskproperties>> DeleteData(Guid id)
        {
            var deletedata = await Taskmanager.DeleteData(id);
            return deletedata is not null ? Ok(deletedata) : BadRequest("No data");
        }

    }

}

