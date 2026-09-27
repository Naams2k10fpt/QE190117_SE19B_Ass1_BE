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
}