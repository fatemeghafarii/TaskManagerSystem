using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Core.Abstractions;

namespace TaskManager.Common.Persistence.EF;

public interface IRepository<TAggregate, TKey> where TAggregate : Entity<TKey>
{
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(TAggregate task, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}