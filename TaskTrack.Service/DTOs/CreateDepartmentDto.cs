using System.ComponentModel.DataAnnotations;

namespace TaskTrack.Service.DTOs;

public sealed class CreateDepartmentDto
{
    [Required(ErrorMessage = "Department name is required.")]
    [MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department description is required.")]
    [MaxLength(300)]
    public string DepartmentDescription { get; set; } = string.Empty;
}