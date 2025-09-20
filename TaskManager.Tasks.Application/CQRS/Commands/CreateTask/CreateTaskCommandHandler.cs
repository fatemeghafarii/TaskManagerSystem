using MediatR;
using TaskManager.Tasks.Application.Interfaces.Persistence;
using DomainTask = TaskManager.Tasks.Domain.Entities.Task;

namespace TaskManager.Tasks.Application.CQRS.Commands.CreateTask;
public class CreateTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new DomainTask
        (
            title: request.Title,
            description: request.Description,
            userId: request.UserId

        );
        await _taskRepository.CreateAsync(task, cancellationToken);
        await _taskRepository.SaveChangesAsync(cancellationToken);

        return task.Id;
    }
}

