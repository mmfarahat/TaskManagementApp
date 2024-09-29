using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Features.Accounts.Login;
using TaskManagementApp.Application.Features.Accounts.Queries.GetAllUsers;

namespace TaskManagementApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginCommandResponse>> Login([FromBody] LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);
            return Ok(result);
        }
        [HttpGet("All")]
        [Authorize]
        public async Task<ActionResult<List<UsersListDTO>>> Get()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }
    }
}
