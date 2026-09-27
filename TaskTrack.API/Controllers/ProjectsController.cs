using Microsoft.AspNetCore.Mvc;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

namespace TaskTrack.API.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(IProjectService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectListItemDto>>> GetActive()
    {
        var projects = await _service.GetActiveAsync();
        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDetailDto>> GetById(int id)
    {
        var project = await _service.GetByIdAsync(id);

        if (project is null)
            return NotFound();

        return Ok(project);
    }

    [HttpGet("department/{departmentId:int}")]
    public async Task<ActionResult<List<ProjectListItemDto>>> GetByDepartment(
    int departmentId)
    {
        var projects = await _service.GetByDepartmentAsync(departmentId);
        return Ok(projects);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ProjectListItemDto>>> Search(
    [FromQuery] string? name,
    [FromQuery] short? status,
    [FromQuery] int? departmentId)
    {
        if (status.HasValue && (status.Value < 0 || status.Value > 3))
            ModelState.AddModelError("status", "Status must be 0–3.");

        if (departmentId.HasValue && departmentId.Value <= 0)
            ModelState.AddModelError(
                "departmentId", "DepartmentId must be positive.");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var projects = await _service.SearchAsync(
            name, status, departmentId);

        return Ok(projects);
    }
}