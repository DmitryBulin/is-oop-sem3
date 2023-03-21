namespace Application.Dto;

public record WorkerReportDto(
    Guid Id,
    DateTime CreationTime,
    WorkerDto Worker,
    IReadOnlyCollection<ReportRowDto> ReportRows);