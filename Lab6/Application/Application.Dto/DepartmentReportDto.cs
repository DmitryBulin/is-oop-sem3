namespace Application.Dto;

public record DepartmentReportDto(
    Guid Id,
    DateTime CreationTime,
    WorkerDepartmentDto Department,
    IReadOnlyCollection<WorkerReportDto> WorkerReports);