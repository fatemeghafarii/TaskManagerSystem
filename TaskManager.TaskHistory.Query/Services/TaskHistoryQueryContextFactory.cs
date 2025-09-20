using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.TaskHistory.Query.Services;
public class TaskHistoryQueryContextFactory : IDesignTimeDbContextFactory<TaskHistoryQueryContext>
{
    public TaskHistoryQueryContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TaskHistoryQueryContext>();
        builder.UseSqlServer("Server=.\\SQL2025;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True;");
        return new TaskHistoryQueryContext(builder.Options);
    }
}

