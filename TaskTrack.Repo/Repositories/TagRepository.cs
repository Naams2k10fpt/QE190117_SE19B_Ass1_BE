using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskTrack.Repo.Data;
using TagEntity = TaskTrack.Repo.Models.Tag;

namespace TaskTrack.Repo.Repositories;

public class TagRepository : ITagRepository
{
    private readonly TaskTrackDbContext _context;

    public TagRepository(TaskTrackDbContext context)
    {
        _context = context;
    }

    public Task<List<TagEntity>> GetAllAsync()
    {
        return _context.Tags
            .AsNoTracking()
            .OrderBy(tag => tag.TagName)
            .ToListAsync();
    }
}