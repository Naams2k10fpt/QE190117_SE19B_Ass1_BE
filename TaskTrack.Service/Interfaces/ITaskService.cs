using TaskTrack.Service.DTOs;

namespace TaskTrack.Service.Interfaces;

public interface ITaskService
{
    Task<List<TaskListItemDto>> GetActiveAsync();
}