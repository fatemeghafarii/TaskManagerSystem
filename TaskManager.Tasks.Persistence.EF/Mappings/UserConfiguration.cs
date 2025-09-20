using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Tasks.Domain.Entities;

namespace TaskManager.Tasks.Persistence.EF.Mappings;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(t => t.UserName)
               .IsRequired()
               .HasMaxLength(200)
               .HasColumnType("nvarchar");
    }
}
