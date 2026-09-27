namespace TaskTrack.Service.DTOs;

public sealed record ProjectTagDto(
    int TagId,
    string TagName,
    string? Color);

public sealed record ProjectTaskDto(
    int TaskId,
    string Title,
    short Status,
    short Priority,
    DateOnly? DueDate,
    List<ProjectTagDto> Tags);

public sealed record ProjectDetailDto(
    int ProjectId,
    string ProjectName,
    string? Description,
    DateOnly StartDate,
    DateOnly? EndDate,
    short Status,
    int DepartmentId,
    string DepartmentName,
    bool IsActive,
    DateTime CreatedDate,
    List<ProjectTaskDto> Tasks);