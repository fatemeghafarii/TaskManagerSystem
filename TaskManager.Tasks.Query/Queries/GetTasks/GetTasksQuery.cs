using MediatR;
using TaskManager.Tasks.Query.Contract.Dtos;

namespace TaskManager.Tasks.Query.Queries.GetTasks;
public record GetTasksQuery() : IRequest<List<TaskDto>>;
