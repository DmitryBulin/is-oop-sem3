using Domain.Devices;
using Domain.Messages;
using Domain.Users;
using RichEntity.Annotations;

namespace Domain.Reports;

#pragma warning disable SA1601
public partial class WorkerReport : IEntity<Guid>
#pragma warning restore SA1601
{
    public WorkerReport(Guid id, DateTime creationTime, WorkerModel worker, IReadOnlyCollection<ReportRow> reportRows)
        : this(id)
    {
        CreationTime = creationTime;
        Worker = worker;
        ReportRows = reportRows;
    }

    public virtual DateTime CreationTime { get; protected init; }
    public virtual WorkerModel Worker { get; protected init; }
    public virtual IReadOnlyCollection<ReportRow> ReportRows { get; protected init; }

    public static WorkerReport GenerateReport(Worker worker, DateTime creationTime)
    {
        var reportRows = new List<ReportRow>();

        foreach (Device device in worker.Messages.Select(x => x.SenderDevice).Distinct())
        {
            IEnumerable<Message> messages = worker.Messages.Where(x => x.SenderDevice.Equals(device));
            reportRows.Add(new ReportRow(
                Guid.NewGuid(),
                DeviceModel.FromDevice(device),
                new MessageCount(messages is not null ? messages.Count() : 0)));
        }

        return new WorkerReport(
            Guid.NewGuid(),
            creationTime,
            WorkerModel.FromWorker(worker),
            reportRows);
    }
}
