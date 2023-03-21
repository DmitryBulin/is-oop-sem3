using Application.Dto;
using MediatR;

namespace Application.Contracts.Workers;

public static class CreateWorker
{
    public record struct Comand(string Name, string SecondName, Guid DepartmentId)
        : IRequest<Response>;

    public record struct Response(WorkerDto Worker);
}