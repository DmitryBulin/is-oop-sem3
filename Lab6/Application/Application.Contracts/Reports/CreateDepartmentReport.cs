using Application.Dto;
using MediatR;

namespace Application.Contracts.Reports;

public static class CreateDepartmentReport
{
    public record struct Comand(Guid DepartmentId, DateTime ReportTime)
        : IRequest<Response>;

    public record struct Response(DepartmentReportDto DepartmentReport);
}