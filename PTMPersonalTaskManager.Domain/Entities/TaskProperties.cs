using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Domain.Entities
{
    public class TaskProperties
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }     
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public TaskPriority Priority { get; set; }
        public int TotalTasks { get; set; }
        public int TasksCompleted { get; set; }
        public DateTime DueToday { get; set; }
       
        
    }
    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        Hard = 3
    }
}
