using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Domain.Entities;
using AppTask = TaskManagementApp.Domain.Entities.AppTask;

namespace TaskManagementApp.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskCommandResponse>
    {
        IMapper _mapper;
        ITaskRepository _taskRepository;
        public CreateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateTaskCommandResponse();
            var task = _mapper.Map<AppTask>(request);
            var validationResult = await new CreateTaskCommandValidator().ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            else if (response.Success)
            {
                task = await _taskRepository.AddAsync(task);
                response.CreatedTask = _mapper.Map<CreateTaskDTO>(task);
            }

            return response;
        }
    }
}
