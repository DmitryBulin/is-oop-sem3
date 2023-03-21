using Application.Abstractions.DataAccess;
using Application.Mapping;
using Domain.Users;
using MediatR;
using static Application.Contracts.WorkerDepartments.CreateWorkerDepartment;

namespace Application.WorkerDepartments;

internal class CreateWorkerDepartmentHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public CreateWorkerDepartmentHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        var department = new WorkerDepartment(Guid.NewGuid(), new DepartmentName(request.Name));

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(department.AsDto());
    }
}
