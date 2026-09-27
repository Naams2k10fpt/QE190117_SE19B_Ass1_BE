using TaskTrack.Service.DTOs;
using TaskTrack.Service.Results;

namespace TaskTrack.Service.Interfaces;

public interface ITagService
{
    Task<List<TaskTagDto>> GetAllAsync();
    Task<bool> NameExistsAsync(string name, int? excludeId = null);
    Task<TaskTagDto> CreateAsync(CreateTagDto input);
    Task<bool> UpdateAsync(int id, CreateTagDto input);
    Task<DeleteTagResult> DeleteAsync(int id);
}