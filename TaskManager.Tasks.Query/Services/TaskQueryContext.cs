using Microsoft.EntityFrameworkCore;
using DomainTask =  TaskManager.Tasks.Domain.Entities.Task;

namespace TaskManager.Tasks.Query.Services;
public class TaskQueryContext(DbContextOptions<TaskQueryContext> options) : DbContext(options)
{
    public DbSet<DomainTask> Task { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskQueryContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
