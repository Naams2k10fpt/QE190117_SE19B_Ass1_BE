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
    public async Task<Department> AddAsync(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return department;
    }
    public async Task<bool> UpdateAsync(
    int id, string name, string description, bool isActive)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department is null)
            return false;

        department.DepartmentName = name;
        department.DepartmentDescription = description;
        department.IsActive = isActive;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department is null)
            return false;

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return true;
    }
}