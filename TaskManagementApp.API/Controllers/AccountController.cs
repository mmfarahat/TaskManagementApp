using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.API.Chat;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Features.Accounts.Login;
using TaskManagementApp.Application.Features.Accounts.Queries.GetAllUsers;

namespace TaskManagementApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        static List<ConnectedUserModel> _connectionIds = new List<ConnectedUserModel>();
        IMediator _mediator;
        IUserService _userService;
        public AccountController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
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
        [HttpGet("UpdateConnectionId/{Id}")]
        [Authorize]
        public async Task<ActionResult> UpdateConnectionId(string id)
        {
            if (_connectionIds.Any(u => u.UserId == _userService.LoggedInUserId))
            {
                _connectionIds.First(u => u.UserId == _userService.LoggedInUserId).ConnectionId = id;
            }
            else
            {
                var email = await _userService.GetUserName(_userService.LoggedInUserId);
                if (!string.IsNullOrEmpty(email))
                {
                    _connectionIds.Add(new ConnectedUserModel
                    {
                        UserId = _userService.LoggedInUserId,
                        ConnectionId = id,
                        Email = email
                    });
                }
            }
            return Ok();
        }
        [HttpGet("RemoveConnectionId")]
        public async Task<ActionResult> RemoveConnectionId()
        {
            if (_connectionIds.Any(u => u.UserId == _userService.LoggedInUserId))
            {
                _connectionIds.Remove(_connectionIds.First(u => u.UserId == _userService.LoggedInUserId));
            }
            return Ok();
        }
        [HttpGet("GetConnectedUsers")]
        [Authorize]
        public async Task<ActionResult> GetConnectedUsers()
        {
            return Ok(_connectionIds.Where(u => u.UserId != _userService.LoggedInUserId));
        }
    }
}
