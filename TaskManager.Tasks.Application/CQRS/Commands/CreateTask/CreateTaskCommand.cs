using MediatR;

namespace TaskManager.Tasks.Application.CQRS.Commands.CreateTask;
public record CreateTaskCommand(string Title, string Description, Guid UserId) : IRequest<Guid>;
