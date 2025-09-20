using MediatR;
using TaskManager.Common.Core.Abstractions;

namespace TaskManager.Tasks.Domain.Events;
public class TaskStatusChangedEvent : IEvent, INotification
{
    public Guid TaskId { get; set; }
    public string? OldStatus { get; set; }
    public string? NewStatus { get; set; }
    public Guid ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
    public DateTime? PublishedOn { get; set; }
}
