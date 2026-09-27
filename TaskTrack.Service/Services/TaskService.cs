using TaskTrack.Repo.Repositories;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

namespace TaskTrack.Service.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskListItemDto>> GetActiveAsync()
    {
        var tasks = await _repository.GetActiveWithProjectAsync();

        return tasks
            .Select(t => new TaskListItemDto(
                t.TaskId,
                t.Title,
                t.Description,
                t.Status,
                t.Priority,
                t.DueDate,
                t.ProjectId,
                t.Project.ProjectName,
                t.CreatedDate,
                t.ModifiedDate))
            .ToList();
    }
    public async Task<TaskDetailDto?> GetByIdAsync(int id)
    {
        var task = await _repository.GetByIdWithDetailsAsync(id);

        if (task is null)
            return null;

        var tags = task.Tags
            .OrderBy(tag => tag.TagName)
            .Select(tag => new TaskTagDto(
                tag.TagId, tag.TagName, tag.Color))
            .ToList();

        return new TaskDetailDto(
            task.TaskId,
            task.Title,
            task.Description,
            task.Status,
            task.Priority,
            task.DueDate,
            task.ProjectId,
            task.Project.ProjectName,
            task.IsActive,
            task.CreatedDate,
            task.ModifiedDate,
            tags);
    }
}