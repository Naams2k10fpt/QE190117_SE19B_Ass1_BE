using System.ComponentModel.DataAnnotations;

namespace TaskTrack.Service.DTOs;

public sealed class CreateProjectDto
{
    [Required]
    [MaxLength(200)]
    public string ProjectName { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Required]
    [Range(0, 3)]
    public short? Status { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int? DepartmentId { get; set; }
}