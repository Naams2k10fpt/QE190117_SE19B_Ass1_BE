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
}