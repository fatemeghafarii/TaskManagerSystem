using MediatR;
using TaskManager.TaskHistory.Query.Contract.Dtos;

namespace TaskManager.TaskHistory.Query.Queries.GetTaskHistoriesByTaskId;
public record GetTaskHistoryByTaskIdQuery(Guid TaskId) : IRequest<List<TaskHistoryDto>>;
