using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Accounts.Queries.GetAllUsers;
using TaskManagementApp.Application.Models;
namespace TaskManagementApp.Application.Contracts
{
    public interface IUserService
    {
        public string LoggedInUserId { get; }
        public Task<bool> VerifyUser(string userId);
        public Task<string> GetUserName(string userId);
        public Task<TokenResponse?> LogUserIn(string email,string password);
        public Task<List<UsersListDTO>> GetAllUsers();

    }
}
