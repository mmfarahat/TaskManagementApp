using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Application.Features.Accounts.Queries.GetAllUsers
{
    public class GetAllUsersQuery:IRequest<List<UsersListDTO>>
    {
    }
}
