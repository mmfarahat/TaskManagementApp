using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, GetTaskListQueryResponse>
    {
        IMapper _mapper;
        ITaskRepository _taskRepository;

        public GetTaskListQueryHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;

        }
        public async Task<GetTaskListQueryResponse> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.SearchTasks(request);

        }


    }
}
