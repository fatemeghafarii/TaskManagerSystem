using MediatR;
using TaskManager.Tasks.Query.Contract.Dtos;

namespace TaskManager.Tasks.Query.Queries.GetTaskById;
public record GetTaskByIdQuery(Guid Id) : IRequest<TaskDto>;
