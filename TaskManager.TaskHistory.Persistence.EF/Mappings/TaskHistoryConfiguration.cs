using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskChangeLog = TaskManager.TaskHistory.Domain.Entities.TaskHistory;

namespace TaskManager.TaskHistory.Persistence.EF.Mappings;
public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskChangeLog>
{
    public void Configure(EntityTypeBuilder<TaskChangeLog> builder)
    {
        builder.Property(p => p.TaskId)
               .IsRequired();

        builder.Property(p => p.OldStatus)
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("nvarchar");
        
        builder.Property(p => p.NewStatus)
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("nvarchar");

        builder.Property(p => p.ChangedBy)
               .IsRequired()
               .HasColumnType("uniqueidentifier");

        builder.Property(x => x.ChangedAt)
               .IsRequired();
    }
}
