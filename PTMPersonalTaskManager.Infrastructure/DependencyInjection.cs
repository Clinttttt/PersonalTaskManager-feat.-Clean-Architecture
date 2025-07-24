using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTMPersonalTaskManager.Domain.Interfaces;
using PTMPersonalTaskManager.Infrastructure.Data;
using PTMPersonalTaskManager.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure
{
  public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITaskmanager, Taskmanager>();
            return services;
        }

    }
}
