using System.ComponentModel.DataAnnotations;

namespace TaskTrack.Service.DTOs;

public sealed class UpdateDepartmentDto
{
    [Required]
    [MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string DepartmentDescription { get; set; } = string.Empty;

    [Required]
    public bool? IsActive { get; set; }
}