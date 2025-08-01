using PTMPersonalTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Domain.DTOs.DetailsDto
{
   public class DetailsDto
    {
     public Guid Id { get; set; }
     public Guid UserId { get; set; }
     public string? Title { get; set; }
     public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public TaskPriority Priority { get; set; }
    }
}
