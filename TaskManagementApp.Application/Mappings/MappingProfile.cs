using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Features.Tasks.Commands.CreateTask;
using TaskManagementApp.Application.Features.Tasks.Commands.UpdateTask;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
       
        public MappingProfile()
        {
           
            CreateMap<Domain.Entities.AppTask, CreateTaskDTO>().ReverseMap();
            CreateMap<Domain.Entities.AppTask, UpdateTaskDTO>().ReverseMap();
            CreateMap<Domain.Entities.AppTask, CreateTaskCommand>().ReverseMap();
            CreateMap<Domain.Entities.AppTask, UpdateTaskCommand>().ReverseMap();
            CreateMap<Domain.Entities.AppTask, GetTaskDTO>().AfterMap<GetUserName>().ReverseMap();
            CreateMap<Domain.Entities.AppTask, GetTaskByIdQuery>().ReverseMap();
           // CreateMap<Domain.Entities.AppTask, TaskListDTO>().AfterMap<GetUserName>().ReverseMap();
        }
    }

    public class GetUserName : IMappingAction<AppTask, GetTaskDTO>
    {
        private readonly IUserService _userService;

        public GetUserName(IUserService userService)
        {
            _userService = userService;
        }

        public void Process(AppTask source, GetTaskDTO destination, ResolutionContext context)
        {
            destination.AssignedToName =  _userService.GetUserName(source.AssignedTo).Result;
        }
    }
}
