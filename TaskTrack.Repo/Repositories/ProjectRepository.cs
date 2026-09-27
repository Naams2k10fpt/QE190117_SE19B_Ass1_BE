using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTrack.Repo.Data;
using Project = TaskTrack.Repo.Models.Project;

namespace TaskTrack.Repo.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly TaskTrackDbContext _context;

    public ProjectRepository(TaskTrackDbContext context)
    {
        _context = context;
    }

    public Task<List<Project>> GetActiveWithDepartmentAsync()
    {
        return _context.Projects
            .AsNoTracking()
            .Include(p => p.Department)
            .Where(p => p.IsActive)
            .OrderBy(p => p.ProjectName)
            .ToListAsync();
    }
    public Task<Project?> GetByIdWithDetailsAsync(int id)
    {
        return _context.Projects
            .AsNoTracking()
            .Include(p => p.Department)
            .Include(p => p.Tasks)
                .ThenInclude(t => t.Tags)
            .FirstOrDefaultAsync(p => p.ProjectId == id);
    }
    public Task<List<Project>> GetByDepartmentAsync(int departmentId)
    {
        return _context.Projects
            .AsNoTracking()
            .Include(p => p.Department)
            .Where(p => p.IsActive && p.DepartmentId == departmentId)
            .OrderBy(p => p.ProjectName)
            .ToListAsync();
    }
    public Task<List<Project>> SearchAsync(
    string? name, short? status, int? departmentId)
    {
        IQueryable<Project> query = _context.Projects
            .AsNoTracking()
            .Include(p => p.Department)
            .Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(name))
        {
            var pattern = $"%{name.Trim()}%";
            query = query.Where(p =>
                EF.Functions.ILike(p.ProjectName, pattern));
        }

        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        if (departmentId.HasValue)
            query = query.Where(p => p.DepartmentId == departmentId.Value);

        return query
            .OrderBy(p => p.ProjectName)
            .ToListAsync();
    }
    public Task<bool> DepartmentExistsAsync(int departmentId)
    {
        return _context.Departments
            .AnyAsync(d => d.DepartmentId == departmentId);
    }

    public async Task<Project> AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }
    public async Task<bool> UpdateAsync(int id, Project changes)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project is null)
            return false;

        project.ProjectName = changes.ProjectName;
        project.Description = changes.Description;
        project.StartDate = changes.StartDate;
        project.EndDate = changes.EndDate;
        project.Status = changes.Status;
        project.DepartmentId = changes.DepartmentId;
        project.IsActive = changes.IsActive;

        await _context.SaveChangesAsync();
        return true;
    }
}