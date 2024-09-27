using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository<AppTask>, ITaskRepository
    {
        public TaskRepository(TaskManagementDBContext dbContext) : base(dbContext)
        {
        }
    }
}
