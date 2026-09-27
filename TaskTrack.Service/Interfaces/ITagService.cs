using TaskTrack.Service.DTOs;

namespace TaskTrack.Service.Interfaces;

public interface ITagService
{
    Task<List<TaskTagDto>> GetAllAsync();
    Task<bool> NameExistsAsync(string name, int? excludeId = null);
    Task<TaskTagDto> CreateAsync(CreateTagDto input);
}