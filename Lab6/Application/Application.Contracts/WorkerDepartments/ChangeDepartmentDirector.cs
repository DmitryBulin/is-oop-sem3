using MediatR;

namespace Application.Contracts.WorkerDepartments;

public static class ChangeDepartmentDirector
{
    public record struct Comand(Guid DepartmentId, Guid DirectorId)
        : IRequest;
}