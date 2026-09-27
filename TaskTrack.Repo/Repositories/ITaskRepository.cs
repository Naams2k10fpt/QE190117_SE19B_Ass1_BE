using System.Collections.Generic;
using System.Threading.Tasks;
using TaskEntity = TaskTrack.Repo.Models.Task;

namespace TaskTrack.Repo.Repositories;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetActiveWithProjectAsync();
    Task<TaskEntity?> GetByIdWithDetailsAsync(int id);
    Task<List<TaskEntity>> GetByProjectAsync(int projectId);
    Task<List<TaskEntity>> SearchAsync(
    string? title, short? status, short? priority,
    int? projectId, int? tagId);
}