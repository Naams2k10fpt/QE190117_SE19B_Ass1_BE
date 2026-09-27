using System.ComponentModel.DataAnnotations;

namespace TaskTrack.Service.DTOs;

public sealed class CreateTagDto
{
    [Required(ErrorMessage = "Tag name is required.")]
    [MaxLength(50)]
    public string TagName { get; set; } = string.Empty;

    [RegularExpression(
        @"^#[0-9A-Fa-f]{6}$",
        ErrorMessage = "Color must be in #RRGGBB format.")]
    public string? Color { get; set; }
}