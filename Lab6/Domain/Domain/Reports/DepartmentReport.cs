using Domain.Users;
using RichEntity.Annotations;

namespace Domain.Reports;

#pragma warning disable SA1601
public partial class DepartmentReport : IEntity<Guid>
#pragma warning restore SA1601
{
    public DepartmentReport(Guid id, DateTime creationTime, DepartmentModel department, IReadOnlyCollection<WorkerReport> workerReports)
        : this(id)
    {
        CreationTime = creationTime;
        Department = department;
        WorkerReports = workerReports;
    }

    public virtual DateTime CreationTime { get; protected init; }
    public virtual DepartmentModel Department { get; protected init; }
    public virtual IReadOnlyCollection<WorkerReport> WorkerReports { get; protected init; }

    public static DepartmentReport GenerateReport(WorkerDepartment department, DateTime creationTime)
    {
        return new DepartmentReport(
            Guid.NewGuid(),
            creationTime,
            DepartmentModel.FromDepartment(department),
            department.Workers.Select(x => WorkerReport.GenerateReport(x, creationTime)).ToArray());
    }
}
