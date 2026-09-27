using System.Collections.Generic;
using System.Threading.Tasks;
using TaskEntity = TaskTrack.Repo.Models.Task;

namespace TaskTrack.Repo.Repositories;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetActiveWithProjectAsync();
    Task<TaskEntity?> GetByIdWithDetailsAsync(int id);
}