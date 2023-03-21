using Application.Dto;
using Domain.Reports;

namespace Application.Mapping;

public static class ReportMapping
{
    public static ReportRowDto AsDto(this ReportRow reportRow)
        => new ReportRowDto(
            reportRow.Id,
            reportRow.Device.Id,
            reportRow.Count.Value);

    public static WorkerReportDto AsDto(this WorkerReport report)
        => new WorkerReportDto(
            report.Id,
            report.CreationTime,
            report.Worker.AsDto(),
            report.ReportRows.Select(x => x.AsDto()).ToArray());

    public static DepartmentReportDto AsDto(this DepartmentReport report)
        => new DepartmentReportDto(
            report.Id,
            report.CreationTime,
            report.Department.AsDto(),
            report.WorkerReports.Select(x => x.AsDto()).ToArray());
}
