using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Domain.Interfaces
{
    public interface ITaskmanager
    {

        Task<DetailsDto?> CreateData(TaskProperties create);
        Task<IEnumerable<TaskProperties>> ListData();
        Task<TaskProperties?> TaskReadData(Guid id);
        Task<TaskProperties?> UpdateData(DetailsDto update);
        Task<TaskProperties?> DeleteData(Guid id);
    }
}
