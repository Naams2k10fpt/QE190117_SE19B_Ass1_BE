using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTrack.Repo.Data;
using TaskEntity = TaskTrack.Repo.Models.Task;

namespace TaskTrack.Repo.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskTrackDbContext _context;

    public TaskRepository(TaskTrackDbContext context)
    {
        _context = context;
    }

    public Task<List<TaskEntity>> GetActiveWithProjectAsync()
    {
        return _context.Tasks
            .AsNoTracking()
            .Include(t => t.Project)
            .Where(t => t.IsActive)
            .OrderBy(t => t.TaskId)
            .ToListAsync();
    }
    public Task<TaskEntity?> GetByIdWithDetailsAsync(int id)
    {
        return _context.Tasks
            .AsNoTracking()
            .Include(t => t.Project)
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.TaskId == id && t.IsActive);
    }
    public Task<List<TaskEntity>> GetByProjectAsync(int projectId)
    {
        return _context.Tasks
            .AsNoTracking()
            .Include(t => t.Project)
            .Where(t => t.IsActive && t.ProjectId == projectId)
            .OrderBy(t => t.TaskId)
            .ToListAsync();
    }
}