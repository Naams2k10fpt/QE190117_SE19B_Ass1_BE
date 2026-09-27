using TaskTrack.Service.DTOs;

namespace TaskTrack.Service.Interfaces;

public interface IProjectService
{
    Task<List<ProjectListItemDto>> GetActiveAsync();
    Task<ProjectDetailDto?> GetByIdAsync(int id);
}