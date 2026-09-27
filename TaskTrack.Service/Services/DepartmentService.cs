using TaskTrack.Repo.Repositories;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

namespace TaskTrack.Service.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DepartmentListItemDto>> GetActiveAsync()
    {
        var departments = await _repository.GetActiveAsync();

        return departments
            .Select(d => new DepartmentListItemDto(
                d.DepartmentId,
                d.DepartmentName,
                d.DepartmentDescription))
            .ToList();
    }
    public async Task<DepartmentDetailDto?> GetByIdAsync(int id)
    {
        var department = await _repository.GetByIdWithProjectsAsync(id);

        if (department is null)
            return null;

        var projects = department.Projects
            .OrderBy(p => p.ProjectName)
            .Select(p => new DepartmentProjectDto(
                p.ProjectId,
                p.ProjectName,
                p.Status,
                p.IsActive))
            .ToList();

        return new DepartmentDetailDto(
            department.DepartmentId,
            department.DepartmentName,
            department.DepartmentDescription,
            department.IsActive,
            projects);
    }
    public async Task<List<DepartmentListItemDto>> SearchByNameAsync(string name)
    {
        var departments = await _repository.SearchByNameAsync(name);

        return departments
            .Select(d => new DepartmentListItemDto(
                d.DepartmentId,
                d.DepartmentName,
                d.DepartmentDescription))
            .ToList();
    }
}