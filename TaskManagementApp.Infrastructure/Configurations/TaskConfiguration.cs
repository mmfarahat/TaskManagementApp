using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementApp.Domain.Entities;

namespace TaskManagementApp.Infrastructure.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<AppTask>
    {
        public void Configure(EntityTypeBuilder<AppTask> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Title)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(builder => builder.Description)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(builder => builder.DueDate)
                .IsRequired();
            builder.Property(builder => builder.Priority)
                .IsRequired();
            builder.Property(builder => builder.Status)
                .IsRequired();

        }

    }
}
