using TaskManager.TaskHistory.Query.Contract.Dtos;

namespace TaskManager.TaskHistory.Query.Contract.IServices;
public interface ITaskHistoryQueryService
{
    Task<List<TaskHistoryDto>> GetTaskHistoryByTaskIdAsync(Guid taskId, CancellationToken cancellationToken);
}
