using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Data;
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
        public async Task<IEnumerable<TaskProperties>> ListData()
        {
            return await context.taskProperties.ToListAsync();
        }


        public async Task<DetailsDto?> CreateData(TaskProperties create)
        {
        
            var Details = new TaskProperties
            {
                Title = create.Title,
                Description = create.Description,
                StartDate = create.StartDate,
                DueDate = create.DueDate
            };
            context.taskProperties.Add(Details);
            await context.SaveChangesAsync();

            return Details.Adapt<DetailsDto>();
        }
        public async Task<TaskProperties?> TaskReadData(Guid id)
        {
            var find = await context.taskProperties.FirstOrDefaultAsync(u => u.Id == id);
            if (find is null)
            {
                return null;
            }
            return find;
        }
        public async Task<TaskProperties?> UpdateData(DetailsDto update)
        {
            var find = await context.taskProperties.FindAsync(update.Id);
            if (find is null)
            {
                return null;
            }

            find.Title = update.Title;
            find.Description = update.Description;
            find.DueDate = update.DueDate;

            context.Update(find);
            await context.SaveChangesAsync();
            return find.Adapt<TaskProperties>();
        }
        public async Task<TaskProperties?> DeleteData(Guid id)
        {
            var delete = await context.taskProperties.FindAsync(id);
            if (delete is null)
            {
                return null;
            }
            context.taskProperties.Remove(delete);
            await context.SaveChangesAsync();
            return delete;
        }




    }
}


