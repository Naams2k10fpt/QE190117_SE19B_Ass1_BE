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
}