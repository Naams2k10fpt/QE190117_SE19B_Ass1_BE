using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTrack.Repo.Data;
using Department = TaskTrack.Repo.Models.Department;

namespace TaskTrack.Repo.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly TaskTrackDbContext _context;

    public DepartmentRepository(TaskTrackDbContext context)
    {
        _context = context;
    }

    public Task<List<Department>> GetActiveAsync()
    {
        return _context.Departments
            .AsNoTracking()
            .Where(d => d.IsActive)
            .OrderBy(d => d.DepartmentName)
            .ToListAsync();
    }
    public Task<Department?> GetByIdWithProjectsAsync(int id)
    {
        return _context.Departments
            .AsNoTracking()
            .Include(d => d.Projects)
            .FirstOrDefaultAsync(d => d.DepartmentId == id);
    }
    public Task<List<Department>> SearchByNameAsync(string name)
    {
        return _context.Departments
            .AsNoTracking()
            .Where(d => d.IsActive &&
                EF.Functions.ILike(d.DepartmentName, $"%{name}%"))
            .OrderBy(d => d.DepartmentName)
            .ToListAsync();
    }
}