using TaskTrack.Service.DTOs;
using TaskTrack.Service.Results;

namespace TaskTrack.Service.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentListItemDto>> GetActiveAsync();
    Task<DepartmentDetailDto?> GetByIdAsync(int id);
    Task<List<DepartmentListItemDto>> SearchByNameAsync(string name);
    Task<DepartmentDetailDto> CreateAsync(CreateDepartmentDto input);
    Task<bool> UpdateAsync(int id, UpdateDepartmentDto input);
    Task<DeleteDepartmentResult> DeleteAsync(int id);
}