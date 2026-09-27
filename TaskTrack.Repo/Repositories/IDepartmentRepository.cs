using System.Collections.Generic;
using System.Threading.Tasks;
using Department = TaskTrack.Repo.Models.Department;

namespace TaskTrack.Repo.Repositories;

public interface IDepartmentRepository
{
    Task<List<Department>> GetActiveAsync();
    Task<Department?> GetByIdWithProjectsAsync(int id);
    Task<List<Department>> SearchByNameAsync(string name);
    Task<Department> AddAsync(Department department);
}