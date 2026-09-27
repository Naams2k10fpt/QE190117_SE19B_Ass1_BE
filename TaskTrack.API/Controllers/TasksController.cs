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
}