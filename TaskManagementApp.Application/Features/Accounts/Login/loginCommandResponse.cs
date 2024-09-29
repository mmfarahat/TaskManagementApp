using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Models;

namespace TaskManagementApp.Application.Features.Accounts.Login
{
    public class LoginCommandResponse : BaseResponse
    {
        public LoginCommandResponse() : base()
        {


        }
        public TokenResponse TokenResponse { get; set; } = new TokenResponse();

    }
}
