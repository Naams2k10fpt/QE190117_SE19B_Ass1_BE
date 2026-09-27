using TaskTrack.Repo.Repositories;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

namespace TaskTrack.Service.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProjectListItemDto>> GetActiveAsync()
    {
        var projects = await _repository.GetActiveWithDepartmentAsync();

        return projects
            .Select(p => new ProjectListItemDto(
                p.ProjectId,
                p.ProjectName,
                p.Description,
                p.StartDate,
                p.EndDate,
                p.Status,
                p.DepartmentId,
                p.Department.DepartmentName))
            .ToList();
    }
    public async Task<ProjectDetailDto?> GetByIdAsync(int id)
    {
        var project = await _repository.GetByIdWithDetailsAsync(id);

        if (project is null)
            return null;

        var tasks = project.Tasks
            .Where(t => t.IsActive)
            .OrderBy(t => t.TaskId)
            .Select(t => new ProjectTaskDto(
                t.TaskId,
                t.Title,
                t.Status,
                t.Priority,
                t.DueDate,
                t.Tags
                    .OrderBy(tag => tag.TagName)
                    .Select(tag => new ProjectTagDto(
                        tag.TagId, tag.TagName, tag.Color))
                    .ToList()))
            .ToList();

        return new ProjectDetailDto(
            project.ProjectId,
            project.ProjectName,
            project.Description,
            project.StartDate,
            project.EndDate,
            project.Status,
            project.DepartmentId,
            project.Department.DepartmentName,
            project.IsActive,
            project.CreatedDate,
            tasks);
    }
    public async Task<List<ProjectListItemDto>> GetByDepartmentAsync(
    int departmentId)
    {
        var projects = await _repository.GetByDepartmentAsync(departmentId);

        return projects
            .Select(p => new ProjectListItemDto(
                p.ProjectId,
                p.ProjectName,
                p.Description,
                p.StartDate,
                p.EndDate,
                p.Status,
                p.DepartmentId,
                p.Department.DepartmentName))
            .ToList();
    }
}