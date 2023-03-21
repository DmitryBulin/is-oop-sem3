using Application.Dto;
using MediatR;

namespace Application.Contracts.WorkerDepartments;

public static class CreateWorkerDepartment
{
    public record struct Comand(string Name)
        : IRequest<Response>;

    public record struct Response(WorkerDepartmentDto Department);
}