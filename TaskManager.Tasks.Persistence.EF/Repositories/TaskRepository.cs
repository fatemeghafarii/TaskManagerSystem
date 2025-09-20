using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Persistence.EF;
using TaskManager.Tasks.Application.Interfaces.Persistence;
using TaskManager.Tasks.Persistence.EF.Contexts;
using DomainTask = TaskManager.Tasks.Domain.Entities.Task;
namespace TaskManager.Tasks.Persistence.EF.Repositories;
public class TaskRepository(TaskCommandContext _context) : BaseRepository<DomainTask, Guid>(_context), ITaskRepository
{

    //public async Task<List<DomainTask>> GetAllAsync()
    //{
    //    return await _context.Set<DomainTask>().ToListAsync();
    //}
    public void UpdateAsync(DomainTask aggregate)
    {
        _context.Set<DomainTask>().Update(aggregate);
    }
}
