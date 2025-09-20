using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Common.Persistence.EF.Mappings;
public class OutboxEventItemConfiguration : IEntityTypeConfiguration<OutboxEventItem>
{
    public void Configure(EntityTypeBuilder<OutboxEventItem> builder)
    {
        builder.ToTable("OutboxEventItems");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.EventName).HasMaxLength(200);
        builder.Property(e => e.EventType).HasMaxLength(200);
        builder.Property(e => e.AggregateName).HasMaxLength(200);
        builder.Property(e => e.AggregateType).HasMaxLength(200);
    }
}

