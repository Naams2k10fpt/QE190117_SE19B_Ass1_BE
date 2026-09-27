namespace TaskTrack.Service.DTOs;

public sealed record DepartmentProjectDto(
    int ProjectId,
    string ProjectName,
    short Status,
    bool IsActive);

public sealed record DepartmentDetailDto(
    int DepartmentId,
    string DepartmentName,
    string DepartmentDescription,
    bool IsActive,
    List<DepartmentProjectDto> Projects);