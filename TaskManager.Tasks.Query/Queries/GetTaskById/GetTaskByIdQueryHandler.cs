using MediatR;
using TaskManager.Tasks.Query.Contract.Dtos;
using TaskManager.Tasks.Query.Contract.IServices;

namespace TaskManager.Tasks.Query.Queries.GetTaskById;
public class GetTaskByIdQueryHandler(ITaskQueryService taskQueryService) : IRequestHandler<GetTaskByIdQuery, TaskDto?>
{
    private readonly ITaskQueryService _taskQueryService = taskQueryService;

    public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskQueryService.GetTasksAsync(cancellationToken);
        return tasks.FirstOrDefault(t => t.Id == request.Id);
    }
}
