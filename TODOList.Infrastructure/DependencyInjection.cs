using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Infrastructure.Implementation;

namespace TODOList.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddDbContext<TodoContext>(options => options.UseNpgsql(configuration.GetConnectionString("ToDoDb")));

            services.AddScoped<ITodoRepository, TodoRepository>();

            return services;
        }
    }
}
