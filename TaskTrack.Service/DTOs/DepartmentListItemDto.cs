namespace TaskTrack.Service.DTOs;

public sealed record DepartmentListItemDto(
    int DepartmentId,
    string DepartmentName,
    string DepartmentDescription);