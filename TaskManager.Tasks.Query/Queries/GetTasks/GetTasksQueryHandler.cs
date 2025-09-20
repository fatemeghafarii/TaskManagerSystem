using MediatR;
using TaskManager.Tasks.Query.Contract.Dtos;
using TaskManager.Tasks.Query.Contract.IServices;

namespace TaskManager.Tasks.Query.Queries.GetTasks;

public class GetTasksQueryHandler(ITaskQueryService taskQueryService) : IRequestHandler<GetTasksQuery, List<TaskDto>>
{
    private readonly ITaskQueryService _taskQueryService = taskQueryService;

    public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _taskQueryService.GetTasksAsync(cancellationToken);
    }
}