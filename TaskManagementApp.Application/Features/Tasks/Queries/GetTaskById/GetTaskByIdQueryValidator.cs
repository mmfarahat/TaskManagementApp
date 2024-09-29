using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts.DataAccess;

namespace TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
    {
        public GetTaskByIdQueryValidator(ITaskRepository taskRepository)
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
        }
    }
}
