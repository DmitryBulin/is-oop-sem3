namespace Application.Dto;

public record WorkerDepartmentDto(
    Guid Id,
    string Name,
    WorkerDto Director,
    IReadOnlyCollection<WorkerDto> Workers);