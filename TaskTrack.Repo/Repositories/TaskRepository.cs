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
    public Task<List<TaskEntity>> SearchAsync(
    string? title, short? status, short? priority,
    int? projectId, int? tagId)
    {
        IQueryable<TaskEntity> query = _context.Tasks
            .AsNoTracking()
            .Include(t => t.Project)
            .Where(t => t.IsActive);

        if (!string.IsNullOrWhiteSpace(title))
        {
            var pattern = $"%{title.Trim()}%";
            query = query.Where(t => EF.Functions.ILike(t.Title, pattern));
        }

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (priority.HasValue)
            query = query.Where(t => t.Priority == priority.Value);

        if (projectId.HasValue)
            query = query.Where(t => t.ProjectId == projectId.Value);

        if (tagId.HasValue)
            query = query.Where(t =>
                t.Tags.Any(tag => tag.TagId == tagId.Value));

        return query
            .OrderBy(t => t.TaskId)
            .ToListAsync();
    }
}