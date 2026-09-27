using System.ComponentModel.DataAnnotations;

namespace TaskTrack.Service.DTOs;

public sealed class UpdateTaskDto
{
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [Range(0, 3)]
    public short? Status { get; set; }

    [Required]
    [Range(0, 3)]
    public short? Priority { get; set; }

    public DateOnly? DueDate { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int? ProjectId { get; set; }

    [Required]
    public List<int>? TagIds { get; set; }
}