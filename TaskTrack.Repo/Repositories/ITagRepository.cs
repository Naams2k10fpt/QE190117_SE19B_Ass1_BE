using System.Collections.Generic;
using System.Threading.Tasks;
using TagEntity = TaskTrack.Repo.Models.Tag;

namespace TaskTrack.Repo.Repositories;

public interface ITagRepository
{
    Task<List<TagEntity>> GetAllAsync();
    Task<bool> NameExistsAsync(string name, int? excludeId = null);
    Task<TagEntity> AddAsync(TagEntity tag);
    Task<bool> UpdateAsync(int id, TagEntity changes);
    Task<TagEntity?> GetByIdWithTasksAsync(int id);
    Task<bool> DeleteAsync(int id);
}