using Microsoft.AspNetCore.Mvc;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

namespace TaskTrack.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    public TasksController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskListItemDto>>> GetActive()
    {
        var tasks = await _service.GetActiveAsync();
        return Ok(tasks);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskDetailDto>> GetById(int id)
    {
        var task = await _service.GetByIdAsync(id);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    [HttpGet("project/{projectId:int}")]
    public async Task<ActionResult<List<TaskListItemDto>>> GetByProject(
    int projectId)
    {
        var tasks = await _service.GetByProjectAsync(projectId);
        return Ok(tasks);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<TaskListItemDto>>> Search(
    [FromQuery] string? title,
    [FromQuery] short? status,
    [FromQuery] short? priority,
    [FromQuery] int? projectId,
    [FromQuery] int? tagId)
    {
        if (status.HasValue && (status.Value < 0 || status.Value > 3))
            ModelState.AddModelError("status", "Status must be 0–3.");

        if (priority.HasValue && (priority.Value < 0 || priority.Value > 3))
            ModelState.AddModelError("priority", "Priority must be 0–3.");

        if (projectId.HasValue && projectId.Value <= 0)
            ModelState.AddModelError("projectId", "ProjectId must be positive.");

        if (tagId.HasValue && tagId.Value <= 0)
            ModelState.AddModelError("tagId", "TagId must be positive.");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var tasks = await _service.SearchAsync(
            title, status, priority, projectId, tagId);

        return Ok(tasks);
    }
}