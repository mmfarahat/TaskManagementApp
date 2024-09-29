using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Commands.UpdateTask;
using TaskManagementApp.Domain.Common;
using TaskStatus = TaskManagementApp.Domain.Common.TaskStatus;

namespace TaskManagementApp.Application.Features.Tasks.Commands.CreateTask
{
    public class UpdateTaskCommand : IRequest<UpdateTaskCommandResponse>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public string AssignedTo { get; set; }
    }
}
