using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Application.Features.Tasks.Commands.UpdateTask;
using TaskManagementApp.Domain.Entities;
using AppTask = TaskManagementApp.Domain.Entities.AppTask;

namespace TaskManagementApp.Application.Features.Tasks.Commands.CreateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, UpdateTaskCommandResponse>
    {
        IMapper _mapper;
        ITaskRepository _taskRepository;
        IUserService _userService;
        public UpdateTaskCommandHandler(IMapper mapper, ITaskRepository taskRepository, IUserService userService)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _userService = userService;
        }

        public async Task<UpdateTaskCommandResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
           var response = new UpdateTaskCommandResponse();
            var task = _mapper.Map<AppTask>(request);
            var validationResult = await new UpdateTaskCommandValidator(_userService,_taskRepository).ValidateAsync(request);
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
                task = await _taskRepository.UpdateAsync(task);
                response.UpdatedTask = _mapper.Map<UpdateTaskDTO>(task);
            }
            return response;
        }
    }
}
