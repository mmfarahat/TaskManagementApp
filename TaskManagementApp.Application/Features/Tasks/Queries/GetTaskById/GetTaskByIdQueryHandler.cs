using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Application.Features.Tasks.Commands.CreateTask;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, GetTaskByIdQueryResponse>
    {
        ITaskRepository _taskRepository;
        IMapper _mapper;
        public GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<GetTaskByIdQueryResponse> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetTaskByIdQueryResponse();
            var validationResult = await new GetTaskByIdQueryValidator(_taskRepository).ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
            throw new ValidationException(validationResult);
            }
            else if (response.Success)
            {
                var task = await _taskRepository.GetByIdAsync(request.Id);
                response.Task = _mapper.Map<GetTaskDTO>(task);
            }

            return response;
        }
    }
}