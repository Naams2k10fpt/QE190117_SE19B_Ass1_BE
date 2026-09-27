using TaskTrack.Service.DTOs;

namespace TaskTrack.Service.Interfaces;

public interface ITaskService
{
    Task<List<TaskListItemDto>> GetActiveAsync();
    Task<TaskDetailDto?> GetByIdAsync(int id);
    Task<List<TaskListItemDto>> GetByProjectAsync(int projectId);
    Task<List<TaskListItemDto>> SearchAsync(
    string? title, short? status, short? priority,
    int? projectId, int? tagId);
}