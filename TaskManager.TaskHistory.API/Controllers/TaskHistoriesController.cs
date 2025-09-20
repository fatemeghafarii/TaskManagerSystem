using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.TaskHistory.Query.Contract.Dtos;
using TaskManager.TaskHistory.Query.Queries.GetTaskHistoriesByTaskId;

namespace TaskManager.TaskHistory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskHistoriesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{taskId:guid}")]
        public async Task<ActionResult<List<TaskHistoryDto>>> GetTaskHistoryByTaskId(Guid taskId, CancellationToken cancellation)
        {
            var taskHistory = await _mediator.Send(new GetTaskHistoryByTaskIdQuery(taskId), cancellation);
            return Ok(taskHistory);
        }
    }
}
