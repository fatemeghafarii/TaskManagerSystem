using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.Tasks.Persistence.EF.Contexts;
public class TaskCommandContextFactory : IDesignTimeDbContextFactory<TaskCommandContext>
{
    public TaskCommandContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TaskCommandContext>();
        builder.UseSqlServer("Server=.\\SQL2025;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True;");
        return new TaskCommandContext(builder.Options);
    }
}
