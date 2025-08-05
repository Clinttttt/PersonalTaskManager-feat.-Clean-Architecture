using PTMPersonalTaskManager.Domain.DTOs.DetailsDto;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Client.Services
{
    public class TaskStateService
    {

        public List<DetailsDto>? Task { get; private set; } = new();

        public event Func<Task>? Onchange;

        public async Task CreateTask( List<DetailsDto> task)
        {
            Task = task;
           if(Onchange is not null)
            {
                await Onchange.Invoke();
            }
        }
    }
}
