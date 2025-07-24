using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Domain.DTOs
{
   public class DetailsDto
    {
     public Guid Id { get; set; }
     public string? Title { get; set; }
     public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }

    }
}
