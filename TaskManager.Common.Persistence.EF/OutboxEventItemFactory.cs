using Newtonsoft.Json;
using TaskManager.Common.Core.Abstractions;

namespace TaskManager.Common.Persistence.EF;
public class OutboxEventItemFactory
{
    public static List<OutboxEventItem> Create(IAggregateRoot aggregate)
    {
        var domainEvents = aggregate.GetAllQueuedEvents();

        return domainEvents.Select(e => new OutboxEventItem
        {
            CreatedAt = DateTime.UtcNow,
            EventName = e.GetType().Name,
            EventType = e.GetType().FullName!,
            AggregateName = aggregate.GetType().Name,
            AggregateType = aggregate.GetType().FullName!,
            IsPublished = false,
            Payload = JsonConvert.SerializeObject(e)
        }).ToList();
    }
}
