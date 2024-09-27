using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Domain.Common
{
    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }
    public enum TaskStatus
    {
        Open,
        InProgress,
        Closed
    }
}
