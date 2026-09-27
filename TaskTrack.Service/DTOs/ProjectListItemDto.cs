namespace TaskTrack.Service.DTOs;

public sealed record ProjectListItemDto(
    int ProjectId,
    string ProjectName,
    string? Description,
    DateOnly StartDate,
    DateOnly? EndDate,
    short Status,
    int DepartmentId,
    string DepartmentName);