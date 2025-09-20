using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Core.Abstractions;
using TaskManager.Common.Persistence.EF.Mappings;

namespace TaskManager.Common.Persistence.EF.FrameworkDbContext;
public class FrameworkContext(DbContextOptions options) : DbContext(options)
{
    protected bool SaveDomainEvents { get; set; } = true;
    public DbSet<OutboxEventItem> OutboxEventItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (SaveDomainEvents)
            modelBuilder.ApplyConfiguration(new OutboxEventItemConfiguration());
        else
            modelBuilder.Ignore<OutboxEventItem>();

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleDomainEvents();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void HandleDomainEvents()
    {
        if (!SaveDomainEvents) return;

        var aggregateRoots = ChangeTracker.Entries<IAggregateRoot>()
            .Where(e => e.State != EntityState.Detached && e.Entity.GetAllQueuedEvents().Any())
            .Select(e => e.Entity)
            .ToList();

        var events = aggregateRoots.SelectMany(OutboxEventItemFactory.Create).ToList();
        Set<OutboxEventItem>().AddRange(events);

        aggregateRoots.ForEach(a => a.ClearDomainEvents());    
    }
}

