using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Tasks.Application.CQRS.Commands.CreateTask;
using TaskManager.Tasks.Application.CQRS.Commands.UpdateTaskStatus;
using TaskManager.Tasks.Query.Contract.Dtos;
using TaskManager.Tasks.Query.Queries.GetTaskById;
using TaskManager.Tasks.Query.Queries.GetTasks;

namespace TaskManager.Tasks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateTaskCommand command, CancellationToken cancellation)
    {
        var taskId = await _mediator.Send(command, cancellation);
        return CreatedAtAction(nameof(GetTaskById), new { taskId = taskId }, taskId);
        //return Ok(taskId);
    }

    [HttpPut]
    public async Task<ActionResult> Put(ChangeTaskStatusCommand command, CancellationToken cancellation)
    {
        var taskId = await _mediator.Send(command, cancellation);

        if (taskId == Guid.Empty)
            return BadRequest();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult> Get(CancellationToken cancellation)
    {
        var tasks = await _mediator.Send(new GetTasksQuery(), cancellation);
        return Ok(tasks);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<TaskDto>> GetTaskById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _mediator.Send(new GetTaskByIdQuery(id), cancellationToken);
        if (task == null)
            return NotFound();
        return Ok(task);
    }
}
