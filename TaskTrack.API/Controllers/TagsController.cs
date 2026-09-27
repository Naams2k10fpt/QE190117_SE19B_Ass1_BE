using Microsoft.AspNetCore.Mvc;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

namespace TaskTrack.API.Controllers;

[ApiController]
[Route("api/tags")]
public class TagsController : ControllerBase
{
    private readonly ITagService _service;

    public TagsController(ITagService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskTagDto>>> GetAll()
    {
        var tags = await _service.GetAllAsync();
        return Ok(tags);
    }
    [HttpPost]
    public async Task<ActionResult<TaskTagDto>> Create(
    [FromBody] CreateTagDto input)
    {
        if (await _service.NameExistsAsync(input.TagName))
        {
            ModelState.AddModelError(
                "TagName", "Tag name already exists.");
            return ValidationProblem(ModelState);
        }

        var created = await _service.CreateAsync(input);
        return StatusCode(201, created);
    }
}