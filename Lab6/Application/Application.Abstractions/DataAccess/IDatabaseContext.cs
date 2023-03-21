using Domain.Devices;
using Domain.Messages;
using Domain.Reports;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.DataAccess;

public interface IDatabaseContext
{
    DbSet<Worker> Workers { get; }
    DbSet<WorkerDepartment> Departments { get; }
    DbSet<Message> Messages { get; }
    DbSet<Device> Devices { get; }
    DbSet<DepartmentReport> DepartmentReports { get; }
    DbSet<WorkerReport> WorkerReports { get; }
    DbSet<ReportRow> ReportRows { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
