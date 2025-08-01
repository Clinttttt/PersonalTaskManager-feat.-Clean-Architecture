using Microsoft.EntityFrameworkCore;
using PTMPersonalTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure.Data
{
   public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<TaskProperties> taskProperties { get; set; }
        public DbSet<Profile> profile { get; set; }
        public DbSet<User> user { get; set; }
   
    }
}
