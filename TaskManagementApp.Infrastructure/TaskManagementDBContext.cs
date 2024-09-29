using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Contracts;
using TaskManagementApp.Domain.Common;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Infrastructure
{
    public class TaskManagementDBContext : DbContext
    {
        IUserService _userService;
        public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> options) : base(options)
        {

        }
        public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> options, IUserService userService) : base(options)
        {
            _userService = userService;
        }
        DbSet<AppTask> Tasks { get; set; }
        DbSet<ChatMessage> ChatMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = _userService.LoggedInUserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = _userService.LoggedInUserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
