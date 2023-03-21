using Application.Dto;
using Domain.Users;

namespace Application.Mapping;

public static class WorkerDepartmentMapping
{
    public static WorkerDepartmentDto AsDto(this WorkerDepartment department)
        => new WorkerDepartmentDto(
            department.Id,
            department.Name.Value,
            department.Director is not null ? department.Director.AsDto() : new WorkerDto(Guid.Empty, string.Empty, string.Empty),
            department.Workers.Select(x => x.AsDto()).ToArray());

    public static WorkerDepartmentDto AsDto(this DepartmentModel department)
        => new WorkerDepartmentDto(
            department.Id,
            department.Name.Value,
            department.Director is not null ? department.Director.AsDto() : new WorkerDto(Guid.Empty, string.Empty, string.Empty),
            department.Workers.Select(x => x.AsDto()).ToArray());
}
