using PTMPersonalTaskManager.Domain.DTOs.DetailsDto;
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
        Task<IEnumerable<DetailsDto>?> ListData(Guid Userid);
        Task<DetailsDto?> TaskReadData(Guid id);
        Task<DetailsDto?> UpdateData(UpdateTaskDto update);

        Task<DetailsDto?> DeleteData(Guid id);
    }
}
    