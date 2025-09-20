using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.TaskHistory.Persistence.EF.Contexts;

public class TaskHistoryCommandContextFactory : IDesignTimeDbContextFactory<TaskHistoryCommandContext>
{
    public TaskHistoryCommandContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TaskHistoryCommandContext>();
        builder.UseSqlServer("Server=.\\SQL2025;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True;");
        return new TaskHistoryCommandContext(builder.Options);
    }
}