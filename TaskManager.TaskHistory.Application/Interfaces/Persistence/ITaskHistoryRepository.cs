using TaskChangeLogs = TaskManager.TaskHistory.Domain.Entities.TaskHistory;

namespace TaskManager.TaskHistory.Application.Interfaces.Persistence;
public interface ITaskHistoryRepository
{
    Task CreateAsync(TaskChangeLogs taskHistory, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
