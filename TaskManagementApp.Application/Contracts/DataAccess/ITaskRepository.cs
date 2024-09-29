using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList;
using TaskManagementApp.Domain.Entities;
using AppTask = TaskManagementApp.Domain.Entities.AppTask;

namespace TaskManagementApp.Application.Contracts.DataAccess
{
    public interface ITaskRepository : IAsyncRepository<AppTask>
    {
        public Task<GetTaskListQueryResponse> SearchTasks(GetTaskListQuery request);
    }
}
