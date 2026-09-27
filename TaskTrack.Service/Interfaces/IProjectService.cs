using TaskTrack.Service.DTOs;
using TaskTrack.Service.Results;

namespace TaskTrack.Service.Interfaces;

public interface IProjectService
{
    Task<List<ProjectListItemDto>> GetActiveAsync();
    Task<ProjectDetailDto?> GetByIdAsync(int id);
    Task<List<ProjectListItemDto>> GetByDepartmentAsync(int departmentId);
    Task<List<ProjectListItemDto>> SearchAsync(
    string? name, short? status, int? departmentId);
    Task<bool> DepartmentExistsAsync(int departmentId);
    Task<ProjectDetailDto> CreateAsync(CreateProjectDto input);
    Task<bool> UpdateAsync(int id, UpdateProjectDto input);
    Task<DeleteProjectResult> DeleteAsync(int id);
}