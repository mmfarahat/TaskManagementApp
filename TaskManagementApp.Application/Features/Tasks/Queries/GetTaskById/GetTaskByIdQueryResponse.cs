using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Commands.UpdateTask;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQueryResponse : BaseResponse
    {
        public GetTaskDTO Task { get; set; } = new GetTaskDTO();

    }
}
