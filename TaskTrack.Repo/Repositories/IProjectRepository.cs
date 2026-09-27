using System.Collections.Generic;
using System.Threading.Tasks;
using Project = TaskTrack.Repo.Models.Project;

namespace TaskTrack.Repo.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetActiveWithDepartmentAsync();
    Task<Project?> GetByIdWithDetailsAsync(int id);
    Task<List<Project>> GetByDepartmentAsync(int departmentId);
    Task<List<Project>> SearchAsync(
    string? name, short? status, int? departmentId);
    Task<bool> DepartmentExistsAsync(int departmentId);
    Task<Project> AddAsync(Project project);
    Task<bool> UpdateAsync(int id, Project changes);
    Task<bool> DeleteAsync(int id);
}