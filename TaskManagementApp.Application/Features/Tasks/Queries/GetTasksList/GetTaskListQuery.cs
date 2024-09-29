using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById;
using TaskStatus = TaskManagementApp.Domain.Common.TaskStatus;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList
{
    public class GetTaskListQuery : IRequest<GetTaskListQueryResponse>
    {
        public bool GetMyTasksOnly { get; set; } = false;
        public TaskStatus? TaskStatus { get; set; }
        public string? Title { get; set; }
        public string? Assignee { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
