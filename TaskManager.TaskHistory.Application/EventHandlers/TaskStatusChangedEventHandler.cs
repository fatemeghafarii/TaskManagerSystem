using MediatR;
using TaskManager.TaskHistory.Application.Interfaces.Persistence;
using TaskManager.Tasks.Domain.Events;
using TaskChangeLog = TaskManager.TaskHistory.Domain.Entities.TaskHistory;

namespace TaskManager.TaskHistory.Application.EventHandlers;
public class TaskStatusChangedEventHandler(ITaskHistoryRepository taskHistoryRepository) : INotificationHandler<TaskStatusChangedEvent>
{
    private readonly ITaskHistoryRepository _taskHistoryRepository = taskHistoryRepository;

    public async Task Handle(TaskStatusChangedEvent @event, CancellationToken cancellationToken)
    {
        var taskHistory = new TaskChangeLog
        {
            TaskId = @event.TaskId,
            OldStatus = @event.OldStatus,
            NewStatus = @event.NewStatus,
            ChangedBy = @event.ChangedBy,
            ChangedAt = @event.ChangedAt
        };

        await _taskHistoryRepository.CreateAsync(taskHistory, cancellationToken);
        await _taskHistoryRepository.SaveChangesAsync(cancellationToken);
    }
}
