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
}