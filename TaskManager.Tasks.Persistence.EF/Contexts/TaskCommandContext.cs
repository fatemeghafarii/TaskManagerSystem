using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Persistence.EF;
using TaskManager.Common.Persistence.EF.FrameworkDbContext;
using TaskManager.Tasks.Domain.Entities;
using DomainTask = TaskManager.Tasks.Domain.Entities.Task;

namespace TaskManager.Tasks.Persistence.EF.Contexts;
public class TaskCommandContext(DbContextOptions<TaskCommandContext> options) : FrameworkContext(options)
{
    public DbSet<DomainTask> Task { get; set; }
    public DbSet<OutboxEventItem> OutboxEventItem { get; set; }
    public DbSet<User> User { get; set; }
    //public DbSet<TaskHistory> TaskHistory { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskCommandContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
