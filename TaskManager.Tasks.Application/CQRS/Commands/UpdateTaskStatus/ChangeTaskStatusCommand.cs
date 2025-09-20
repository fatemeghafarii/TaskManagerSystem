using MediatR;
using DomainTaskStatus = TaskManager.Tasks.Domain.Enums.TaskStatus;

namespace TaskManager.Tasks.Application.CQRS.Commands.UpdateTaskStatus;
public record ChangeTaskStatusCommand(Guid TaskId, DomainTaskStatus NewStatus, Guid ChangedBy) : IRequest<Guid>;
