namespace TaskManager.Tasks.Application.Interfaces.Persistence;
using DomainTask = Domain.Entities.Task;
public interface ITaskRepository
{
    //Task<List<DomainTask>> GetAllAsync();
    Task<DomainTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(DomainTask aggregate, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void UpdateAsync(DomainTask aggregate);
}
