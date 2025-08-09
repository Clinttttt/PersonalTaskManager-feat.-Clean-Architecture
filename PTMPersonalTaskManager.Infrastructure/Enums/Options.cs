using PTMPersonalTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure.Enums
{
   public class OptionsEnums
    {
        public string Priority(TaskPriority task)
        {
            return task switch
            {
                TaskPriority.Low => "Fiction",
                TaskPriority.Medium => "Nonfiction ",
                TaskPriority.Hard => "Poetry",
                _ => "Unknown",
            };
        }
    }
}
