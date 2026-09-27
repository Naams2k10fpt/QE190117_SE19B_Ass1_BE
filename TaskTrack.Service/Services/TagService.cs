using TaskTrack.Repo.Repositories;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;
using TaskTrack.Service.Results;

namespace TaskTrack.Service.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;

    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskTagDto>> GetAllAsync()
    {
        var tags = await _repository.GetAllAsync();

        return tags
            .Select(tag => new TaskTagDto(
                tag.TagId,
                tag.TagName,
                tag.Color))
            .ToList();
    }
    public Task<bool> NameExistsAsync(string name, int? excludeId = null)
    {
        return _repository.NameExistsAsync(name, excludeId);
    }

    public async Task<TaskTagDto> CreateAsync(CreateTagDto input)
    {
        var tag = new TaskTrack.Repo.Models.Tag
        {
            TagName = input.TagName.Trim(),
            Color = input.Color?.Trim().ToUpperInvariant()
        };

        var saved = await _repository.AddAsync(tag);

        return new TaskTagDto(
            saved.TagId,
            saved.TagName,
            saved.Color);
    }
    public Task<bool> UpdateAsync(int id, CreateTagDto input)
    {
        var changes = new TaskTrack.Repo.Models.Tag
        {
            TagName = input.TagName.Trim(),
            Color = input.Color?.Trim().ToUpperInvariant()
        };

        return _repository.UpdateAsync(id, changes);
    }
    public async Task<DeleteTagResult> DeleteAsync(int id)
    {
        var tag = await _repository.GetByIdWithTasksAsync(id);

        if (tag is null)
            return DeleteTagResult.NotFound;

        if (tag.Tasks.Any())
            return DeleteTagResult.HasTasks;

        var deleted = await _repository.DeleteAsync(id);

        return deleted
            ? DeleteTagResult.Deleted
            : DeleteTagResult.NotFound;
    }
}