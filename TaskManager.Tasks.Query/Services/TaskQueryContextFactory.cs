using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.Tasks.Query.Services;
public class TaskQueryContextFactory : IDesignTimeDbContextFactory<TaskQueryContext>
{
    public TaskQueryContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TaskQueryContext>();
        builder.UseSqlServer("Server=.\\SQL2025;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True;");
        return new TaskQueryContext(builder.Options);
    }
}

