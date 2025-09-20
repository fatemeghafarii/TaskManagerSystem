using Microsoft.EntityFrameworkCore;
using TaskManager.Tasks.Query.Contract.Dtos;
using TaskManager.Tasks.Query.Contract.IServices;
using DomainTask = TaskManager.Tasks.Domain.Entities.Task;

namespace TaskManager.Tasks.Query.Services;
public class TaskQueryService(TaskQueryContext context) : ITaskQueryService
{
    private readonly TaskQueryContext _context = context;

    public async Task<List<TaskDto>> GetTasksAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<DomainTask>()
                             .Select(t => new TaskDto
                             {
                                 Id = t.Id,
                                 Title = t.Title,
                                 Description = t.Description,
                                 Status = t.Status.ToString(),
                                 UserId = t.UserId
                             })
                             .ToListAsync(cancellationToken);

    }
}
