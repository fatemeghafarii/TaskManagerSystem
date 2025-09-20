using MediatR;
using TaskManager.Tasks.Application.Interfaces.Persistence;
using TaskManager.Tasks.Domain.Exceptions;

namespace TaskManager.Tasks.Application.CQRS.Commands.UpdateTaskStatus;

public class ChangeTaskStatusCommandHandler(ITaskRepository taskRepository) : IRequestHandler<ChangeTaskStatusCommand, Guid>
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Guid> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken)?? throw new TaskNotFoundException("تسکی یافت نشد");
        task.ChangeStatus(request.NewStatus, request.ChangedBy);
        task.UserId = request.ChangedBy;
        _taskRepository.UpdateAsync(task);
        await _taskRepository.SaveChangesAsync(cancellationToken);
        return task.Id;
    }
}
