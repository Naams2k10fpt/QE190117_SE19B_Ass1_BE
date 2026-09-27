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
}