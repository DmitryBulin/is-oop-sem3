using Application.Abstractions.DataAccess;
using Application.Extensions;
using Application.Mapping;
using Domain.Reports;
using Domain.Users;
using MediatR;
using static Application.Contracts.Reports.CreateDepartmentReport;

namespace Application.Reports;

internal class CreateDepartmentReportHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public CreateDepartmentReportHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        WorkerDepartment department = await _context.Departments.GetEntityAsync(request.DepartmentId, cancellationToken);

        var report = DepartmentReport.GenerateReport(department, request.ReportTime);

        _context.DepartmentReports.Add(report);
        foreach (WorkerReport workerReport in report.WorkerReports)
        {
            _context.WorkerReports.Add(workerReport);
            foreach (ReportRow reportRow in workerReport.ReportRows)
            {
                _context.ReportRows.Add(reportRow);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Response(report.AsDto());
    }
}
