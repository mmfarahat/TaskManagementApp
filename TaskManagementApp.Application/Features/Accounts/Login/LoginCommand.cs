using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Commands.CreateTask;

namespace TaskManagementApp.Application.Features.Accounts.Login
{
    public class LoginCommand:IRequest<LoginCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
 
}
