using Microsoft.EntityFrameworkCore;
using System.Threading;
using TaskManager.Common.Core.Abstractions;

namespace TaskManager.Common.Persistence.EF;
public class BaseRepository<TAggregate, TKey> : IRepository<TAggregate, TKey> where TAggregate : Entity<TKey>
{
    private readonly DbContext _context;

    protected BaseRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<TAggregate>().FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }
    public async Task CreateAsync(TAggregate task, CancellationToken cancellationToken)
    {
        await _context.Set<TAggregate>().AddAsync(task, cancellationToken);
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
