using TaskTrack.Repo.Repositories;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;
using TaskTrack.Service.Results;

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
    public async Task<DepartmentDetailDto> CreateAsync(CreateDepartmentDto input)
    {
        var department = new TaskTrack.Repo.Models.Department
        {
            DepartmentName = input.DepartmentName.Trim(),
            DepartmentDescription = input.DepartmentDescription.Trim(),
            IsActive = true
        };

        var saved = await _repository.AddAsync(department);

        return new DepartmentDetailDto(
            saved.DepartmentId,
            saved.DepartmentName,
            saved.DepartmentDescription,
            saved.IsActive,
            new List<DepartmentProjectDto>());
    }
    public Task<bool> UpdateAsync(int id, UpdateDepartmentDto input)
    {
        var isActive = input.IsActive
            ?? throw new ArgumentException("IsActive is required.");

        return _repository.UpdateAsync(
            id,
            input.DepartmentName.Trim(),
            input.DepartmentDescription.Trim(),
            isActive);
    }
    public async Task<DeleteDepartmentResult> DeleteAsync(int id)
    {
        var department = await _repository.GetByIdWithProjectsAsync(id);

        if (department is null)
            return DeleteDepartmentResult.NotFound;

        if (department.Projects.Any())
            return DeleteDepartmentResult.HasProjects;

        var deleted = await _repository.DeleteAsync(id);
        return deleted
            ? DeleteDepartmentResult.Deleted
            : DeleteDepartmentResult.NotFound;
    }
}