using TaskTrack.Service.DTOs;

namespace TaskTrack.Service.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentListItemDto>> GetActiveAsync();
    Task<DepartmentDetailDto?> GetByIdAsync(int id);
    Task<List<DepartmentListItemDto>> SearchByNameAsync(string name);
}