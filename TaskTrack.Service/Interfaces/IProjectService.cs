using TaskTrack.Service.DTOs;

namespace TaskTrack.Service.Interfaces;

public interface IProjectService
{
    Task<List<ProjectListItemDto>> GetActiveAsync();
    Task<ProjectDetailDto?> GetByIdAsync(int id);
    Task<List<ProjectListItemDto>> GetByDepartmentAsync(int departmentId);
    Task<List<ProjectListItemDto>> SearchAsync(
    string? name, short? status, int? departmentId);
}