using TaskTrack.Repo.Repositories;
using TaskTrack.Service.DTOs;
using TaskTrack.Service.Interfaces;

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
}