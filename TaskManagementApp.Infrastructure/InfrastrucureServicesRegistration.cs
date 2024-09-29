using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Infrastructure.Repositories;

namespace TaskManagementApp.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<TaskManagementDBContext>(options =>
                options.UseInMemoryDatabase("TaskManagementAppDB").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                 .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITaskRepository, TaskRepository>();


            return services;
        }
    }
}
