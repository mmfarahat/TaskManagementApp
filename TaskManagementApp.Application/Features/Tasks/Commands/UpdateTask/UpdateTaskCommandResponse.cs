using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Commands.CreateTask;

namespace TaskManagementApp.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandResponse : BaseResponse
    {
        public UpdateTaskCommandResponse() : base()
        {


        }
        public UpdateTaskDTO UpdatedTask { get; set; } = new UpdateTaskDTO();
    }
}
