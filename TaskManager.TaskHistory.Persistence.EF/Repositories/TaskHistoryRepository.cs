using TaskManager.Common.Persistence.EF;
using TaskManager.TaskHistory.Application.Interfaces.Persistence;
using TaskManager.TaskHistory.Persistence.EF.Contexts;
using TaskChangeLogs = TaskManager.TaskHistory.Domain.Entities.TaskHistory;

namespace TaskManager.TaskHistory.Persistence.EF.Repositories;
public class TaskHistoryRepository(TaskHistoryCommandContext _context) : BaseRepository<TaskChangeLogs, Guid>(_context), ITaskHistoryRepository
{
}
