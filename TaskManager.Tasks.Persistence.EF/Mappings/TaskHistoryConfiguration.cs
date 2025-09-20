//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using TaskManager.Tasks.Domain.Entities;

//namespace TaskManager.Tasks.Persistence.EF.Mappings;

//public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
//{
//    public void Configure(EntityTypeBuilder<TaskHistory> builder)
//    {
//        builder.Property(h => h.OldStatus)
//               .IsRequired();
       
//        builder.Property(h => h.NewStatus)
//               .IsRequired();

//        builder.HasOne(h => h.Task)
//               .WithMany(t => t.TaskHistories)
//               .HasForeignKey(e => e.TaskId)
//               .OnDelete(DeleteBehavior.NoAction);
//    }
//}
