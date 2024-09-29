using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts.DataAccess;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTaskById;
using TaskManagementApp.Application.Features.Tasks.Queries.GetTasksList;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository<AppTask>, ITaskRepository
    {
        IMapper _mapper;
        public TaskRepository(TaskManagementDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
        public async Task<GetTaskListQueryResponse> SearchTasks(GetTaskListQuery request)
        {
            var query = _dbContext.Set<AppTask>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(t => t.Title.Contains(request.Title, StringComparison.InvariantCultureIgnoreCase));
            }
            if (request.TaskStatus != null)
            {
                query = query.Where(t => t.Status == request.TaskStatus);
            }
            if (request.Assignee != null)
            {
                query = query.Where(t => t.AssignedTo == request.Assignee);
            }
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
             var tasks = await query.ToListAsync();

            return new GetTaskListQueryResponse
            {
                Tasks = _mapper.Map<List<GetTaskDTO>>(tasks),
                PageCount = totalPages
            };
        }
    }
}
