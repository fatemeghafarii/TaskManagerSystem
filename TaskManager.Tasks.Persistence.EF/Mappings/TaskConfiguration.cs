using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainTask = TaskManager.Tasks.Domain.Entities.Task;

namespace TaskManager.Tasks.Persistence.EF.Mappings;
public class TaskConfiguration : IEntityTypeConfiguration<DomainTask>
{
    public void Configure(EntityTypeBuilder<DomainTask> builder)
    {
        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(200)
               .HasColumnType("nvarchar");

        builder.Property(t => t.Description)
               .IsRequired()
               .HasMaxLength(400)
               .HasColumnType("nvarchar");

        //builder.Property(t => t.Status)
        //        .IsRequired()
        //        .HasConversion<string>();

        builder.Property(t => t.UserId)
               .IsRequired();

        builder.HasOne(t => t.User)
               .WithMany(u => u.Tasks)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
