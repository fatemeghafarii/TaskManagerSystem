using TaskManager.Tasks.Query.Contract.Dtos;

namespace TaskManager.Tasks.Query.Contract.IServices;
public interface ITaskQueryService
{
    Task<List<TaskDto>> GetTasksAsync(CancellationToken cancellationToken);
}
