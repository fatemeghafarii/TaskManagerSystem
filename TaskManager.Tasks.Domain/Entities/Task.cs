using TaskManager.Common.Core.Abstractions;
using TaskManager.Tasks.Domain.Events;
using DomainTaskStatus = TaskManager.Tasks.Domain.Enums.TaskStatus;

namespace TaskManager.Tasks.Domain.Entities;

public partial class Task
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DomainTaskStatus Status { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    //public ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
}
public partial class Task : AggregateRoot<Guid>
{
    public Task()
    {
        
    }
    public Task( string title, string description, Guid userId)
    {
        SetId(Id);
        SetProperties(title, description, userId);
    }
    private void SetId(Guid id)
    {
        Id = id == default ? Guid.NewGuid() : id;
    }
    private void SetProperties(string title, string description, Guid userId)
    {
        Title = title;
        Description = description;
        Status = DomainTaskStatus.NotStarted;
        UserId = userId;
    }
    public void ChangeStatus(DomainTaskStatus newStatus, Guid changedBy)
    {
        var oldStatus = Status;
        Status = newStatus;

        QueueEvent(new TaskStatusChangedEvent       
        {
            TaskId = Id,
            OldStatus = oldStatus.ToString(),
            NewStatus = newStatus.ToString(),
            ChangedBy = changedBy,
            ChangedAt = DateTime.UtcNow
        });
    }
}