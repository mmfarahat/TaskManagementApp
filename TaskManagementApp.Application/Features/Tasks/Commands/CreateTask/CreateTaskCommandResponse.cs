using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandResponse : BaseResponse
    {
        public CreateTaskCommandResponse() : base()
        {


        }
        public CreateTaskDTO CreatedTask { get; set; } = new CreateTaskDTO();

    }
}
