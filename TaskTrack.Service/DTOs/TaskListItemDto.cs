namespace TaskTrack.Service.DTOs;

public sealed record TaskListItemDto(
    int TaskId,
    string Title,
    string? Description,
    short Status,
    short Priority,
    DateOnly? DueDate,
    int ProjectId,
    string ProjectName,
    DateTime CreatedDate,
    DateTime? ModifiedDate);