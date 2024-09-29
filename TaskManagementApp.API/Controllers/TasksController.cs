using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Features.Tasks.Commands.CreateTask;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList;

namespace TaskManagementApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        IMediator _mediator;
        public TasksController(IMediator mediator)
        {
            _mediator = mediator;

        }
        //create task
        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<CreateTaskCommandResponse>> CreateTask(CreateTaskCommand createTaskCommand)
        {
            var result = await _mediator.Send(createTaskCommand);
            return Ok(result);
        }

        [HttpPut(Name = "update")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateTaskCommand updateTaskCommand)
        {
            var task = await _mediator.Send(updateTaskCommand);
            return Ok(task);
        }
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var deleteTaskCommand = new DeleteTaskCommand() { TaskId = id };
        //    var result = await _mediator.Send(deleteTaskCommand);
        //    return Ok(result);
        //}

        [HttpGet("All")]
        [Authorize]
        public async Task<ActionResult> Get([FromQuery] GetTaskListQuery getTaskListQuery)
        {
            var result = await _mediator.Send(getTaskListQuery);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetTaskById(int id)
        {
            var getTaskByIdQuery = new GetTaskByIdQuery() { Id = id };
            var result = await _mediator.Send(getTaskByIdQuery);
            return Ok(result);
        }

    }
}
