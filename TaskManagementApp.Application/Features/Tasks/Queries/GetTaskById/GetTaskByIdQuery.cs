using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQuery : IRequest<GetTaskByIdQueryResponse>
    {
        public long Id { get; set; }
    }
}
