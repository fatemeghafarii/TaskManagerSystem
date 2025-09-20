using MediatR;
using TaskManager.TaskHistory.Query.Contract.Dtos;
using TaskManager.TaskHistory.Query.Contract.IServices;

namespace TaskManager.TaskHistory.Query.Queries.GetTaskHistoriesByTaskId;
public class GetTaskHistoryByTaskIdQueryHandler(ITaskHistoryQueryService taskHistoryQueryService) : IRequestHandler<GetTaskHistoryByTaskIdQuery, List<TaskHistoryDto>>
{
    private readonly ITaskHistoryQueryService _taskHistoryQueryService = taskHistoryQueryService;

    public async Task<List<TaskHistoryDto>> Handle(GetTaskHistoryByTaskIdQuery request, CancellationToken cancellationToken)
    {
        return await _taskHistoryQueryService.GetTaskHistoryByTaskIdAsync(request.TaskId, cancellationToken);
    }
}

