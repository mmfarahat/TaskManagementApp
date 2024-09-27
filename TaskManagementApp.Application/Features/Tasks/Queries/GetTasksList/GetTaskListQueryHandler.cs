using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts.DataAccess;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, List<TaskListDTO>>
    {
        IMapper _mapper;
        ITaskRepository _taskRepository;

        public GetTaskListQueryHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;

        }
        public async Task<List<TaskListDTO>> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
        {
            var allTasks = await _taskRepository.ListAllAsync();
            return _mapper.Map<List<TaskListDTO>>(allTasks);
        }


    }
}
