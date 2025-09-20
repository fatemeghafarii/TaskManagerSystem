namespace TaskManager.Common.Core.Abstractions;
public interface IAggregateRoot
{
    void QueueEvent<TEvent>(TEvent eventToPublish) where TEvent : IEvent;
    IEnumerable<IEvent> GetAllQueuedEvents();
    void ClearDomainEvents();
}
