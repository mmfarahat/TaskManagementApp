using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Application.Features.Tasks.Commands.CreateTask;

namespace TaskManagementApp.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator(IUserService userService, ITaskRepository taskRepository)
        {
            RuleFor(p => p.Id)
                 .NotEmpty().WithMessage("{PropertyName} is required.")
                 .NotNull().CustomAsync(async (id, context, cancellationToken) =>
                 {
                     var task = await taskRepository.GetByIdAsync(id);
                     if (task == null)
                     {
                         context.AddFailure("Id", "Task not found");
                     }
                 });
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");
            RuleFor(p => p.Priority)
                .NotNull()
                .IsInEnum();
            RuleFor(p => p.Status)
                .NotNull()
                .IsInEnum();
            RuleFor(p => p.DueDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(DateTime.Now).WithMessage("{PropertyName} must be a date in the future");
            RuleFor(p => p.AssignedTo).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().CustomAsync(async (assignedTo, context, cancellationToken) =>
                {
                    if (!userService.GetAllUsers().Result.Any(u => u.Id == assignedTo))
                    {
                        context.AddFailure("AssignedToId", "invalid user id");
                    }
                });
        }
    }
}
