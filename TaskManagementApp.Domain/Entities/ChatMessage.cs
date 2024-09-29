using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementApp.Domain.Entities
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsRead { get; set; }=false;
    }
}

