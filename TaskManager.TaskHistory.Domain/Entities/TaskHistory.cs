using TaskManager.Common.Core.Abstractions;

namespace TaskManager.TaskHistory.Domain.Entities;
public class TaskHistory : AuditableEntity<Guid>
{
    public Guid TaskId { get; set; }
    public string? OldStatus { get; set; }
    public string? NewStatus { get; set; }
    public Guid ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
}
