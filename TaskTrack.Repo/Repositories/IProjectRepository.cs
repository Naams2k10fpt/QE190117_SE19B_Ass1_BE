using System.Collections.Generic;
using System.Threading.Tasks;
using Project = TaskTrack.Repo.Models.Project;

namespace TaskTrack.Repo.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetActiveWithDepartmentAsync();
    Task<Project?> GetByIdWithDetailsAsync(int id);
}