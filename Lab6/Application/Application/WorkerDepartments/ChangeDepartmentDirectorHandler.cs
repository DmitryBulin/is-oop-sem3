using Application.Abstractions.DataAccess;
using Application.Extensions;
using Domain.Users;
using MediatR;
using static Application.Contracts.WorkerDepartments.ChangeDepartmentDirector;

namespace Application.WorkerDepartments;

internal class ChangeDepartmentDirectorHandler : IRequestHandler<Comand>
{
    private readonly IDatabaseContext _context;

    public ChangeDepartmentDirectorHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(Comand request, CancellationToken cancellationToken)
    {
        WorkerDepartment department = await _context.Departments.GetEntityAsync(request.DepartmentId, cancellationToken);
        Worker director = await _context.Workers.GetEntityAsync(request.DirectorId, cancellationToken);

        department.ChangeDirector(director);

        _context.Departments.Update(department);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
