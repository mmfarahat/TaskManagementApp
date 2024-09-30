using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList
{
    public class GetTaskListQueryResponse : BaseResponse
    {
        public int totalCount { get; set; }
        public List<GetTaskDTO> Tasks { get; set; } = new List<GetTaskDTO>();
    }
}
