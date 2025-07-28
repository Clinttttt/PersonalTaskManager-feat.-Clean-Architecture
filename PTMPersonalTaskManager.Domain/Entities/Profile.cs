using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Domain.Entities
{
  public class Profile
    {
       public Guid Id { get; set; }
       public string? FullName { get; set; }
       public string? ProfilePicture { get; set; }
       public DateOnly DateCreated { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
