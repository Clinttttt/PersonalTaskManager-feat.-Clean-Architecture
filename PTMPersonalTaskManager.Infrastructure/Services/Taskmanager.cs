using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PTMPersonalTaskManager.Domain.DTOs.DetailsDto;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Data;
using PTMPersonalTaskManager.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure.Services
{
    public class Taskmanager(ApplicationDbContext context) : ITaskmanager
    {
        public async Task<IEnumerable<DetailsDto>?> ListData(Guid Userid)
        {        var task =  await context.taskProperties.Where(u=> u.UserId == Userid).
                ToListAsync();
            return task.Adapt<List<DetailsDto>>();
        }


        public async Task<DetailsDto?> CreateData( TaskProperties create)
        {

            context.taskProperties.Add(create);
            await context.SaveChangesAsync();
            return create.Adapt<DetailsDto>();

        }
        public async Task<DetailsDto?> TaskReadData(Guid id)
        {
            var find = await context.taskProperties.FirstOrDefaultAsync(u => u.Id == id);
            if (find is null)
            {
                return null;
            }
            return find.Adapt<DetailsDto>();
        }
        public async Task<DetailsDto?> UpdateData(UpdateTaskDto update)
        {
            var find = await context.taskProperties.FindAsync(update.Id);
            if (find is null)
            {
                return null;
            }

            find.Title = update.Title;
            find.Description = update.Description;
            find.DueDate = update.DueDate;
            find.IsCompleted = update.IsCompleted;
            find.Priority = update.Priority;

            context.Update(find);
            await context.SaveChangesAsync();
            return find.Adapt<DetailsDto>();
        }
        public async Task<DetailsDto?> DeleteData(Guid id)
        {
            var delete = await context.taskProperties.FindAsync(id);
            if (delete is null)
            {
                return null;
            }
            context.taskProperties.Remove(delete);
            await context.SaveChangesAsync();
            return delete.Adapt<DetailsDto>();
        }




    }
}


