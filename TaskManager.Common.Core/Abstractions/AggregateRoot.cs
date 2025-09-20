namespace TaskManager.Common.Core.Abstractions;
public class AggregateRoot<TKey> : AuditableEntity<TKey>, IAggregateRoot
{
    private readonly List<IEvent> _domainEvents = [];

    public void QueueEvent<TEvent>(TEvent eventToPublish) where TEvent : IEvent
    {
        _domainEvents.Add(eventToPublish);
    }

    public IEnumerable<IEvent> GetAllQueuedEvents() => _domainEvents.AsEnumerable();

    public void ClearDomainEvents() => _domainEvents.Clear();
}
