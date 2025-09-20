using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Persistence.EF.FrameworkDbContext;
using TaskChangeLogs = TaskManager.TaskHistory.Domain.Entities.TaskHistory;

namespace TaskManager.TaskHistory.Persistence.EF.Contexts;
public class TaskHistoryCommandContext(DbContextOptions<TaskHistoryCommandContext> options) : FrameworkContext(options)
{
    public DbSet<TaskChangeLogs> TaskHistory { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskHistoryCommandContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
