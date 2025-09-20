using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Tasks.Domain.Events;

namespace TaskManager.TaskHistory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("task-status-changed")]
        public async Task<IActionResult> TaskStatusChanged([FromBody] TaskStatusChangedEvent ev, CancellationToken cancellationToken)
        {
            await _mediator.Publish(ev, cancellationToken);
            return Ok();
        }
    }
}
