using Microsoft.EntityFrameworkCore;
using TaskManager.TaskHistory.Query.Contract.Dtos;
using TaskManager.TaskHistory.Query.Contract.IServices;

namespace TaskManager.TaskHistory.Query.Services;
public class TaskHistoryQueryService(TaskHistoryQueryContext context) : ITaskHistoryQueryService
{
    private readonly TaskHistoryQueryContext _context = context;

    public async Task<List<TaskHistoryDto>> GetTaskHistoryByTaskIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _context.Set<Domain.Entities.TaskHistory>()
                             .Where(x => x.TaskId == taskId)
                             .Select(t => new TaskHistoryDto
                             {
                                 Id = t.Id,
                                 TaskId = t.TaskId,
                                 OldStatus = t.OldStatus,
                                 NewStatus = t.NewStatus,
                                 ChangedBy = t.ChangedBy,
                                 ChangedAt = t.ChangedAt
                             })
                             .ToListAsync(cancellationToken);
    }
}
