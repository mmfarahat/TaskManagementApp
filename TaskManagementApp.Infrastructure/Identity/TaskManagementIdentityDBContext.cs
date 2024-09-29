using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Infrastructure.Identity
{
    public class TaskManagementIdentityDBContext:IdentityDbContext<AppUser>
    {
        public TaskManagementIdentityDBContext()
        {
                
        }
        public TaskManagementIdentityDBContext(DbContextOptions<TaskManagementIdentityDBContext> options): base(options)
        {
            
        }
       
    }
}
