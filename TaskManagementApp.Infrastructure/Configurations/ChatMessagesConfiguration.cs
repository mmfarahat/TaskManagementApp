using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Infrastructure.Configurations
{
    public class ChatMessagesConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
           builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Message)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(builder => builder.SentAt)
                .IsRequired();
            builder.Property(builder => builder.SenderId)
                .IsRequired();
            builder.Property(builder => builder.ReceiverId)
                .IsRequired();
            builder.Property(builder => builder.IsRead)
                .IsRequired();
        }
    }
}
