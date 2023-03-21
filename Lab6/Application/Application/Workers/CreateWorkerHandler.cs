using Application.Abstractions.DataAccess;
using Application.Extensions;
using Application.Mapping;
using Domain.Users;
using MediatR;
using static Application.Contracts.Workers.CreateWorker;

namespace Application.Workers;

internal class CreateWorkerHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public CreateWorkerHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        WorkerDepartment department = await _context.Departments.GetEntityAsync(request.DepartmentId, cancellationToken);
        var worker = new Worker(
            Guid.NewGuid(),
            new PersonName(request.Name),
            new PersonName(request.SecondName),
            department);

        _context.Workers.Add(worker);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(worker.AsDto());
    }
}
