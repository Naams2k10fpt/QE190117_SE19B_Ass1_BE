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
}