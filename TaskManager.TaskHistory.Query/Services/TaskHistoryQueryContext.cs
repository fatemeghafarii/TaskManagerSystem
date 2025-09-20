using Microsoft.EntityFrameworkCore;

namespace TaskManager.TaskHistory.Query.Services;
public class TaskHistoryQueryContext(DbContextOptions<TaskHistoryQueryContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.TaskHistory> TaskHistory { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskHistoryQueryContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
