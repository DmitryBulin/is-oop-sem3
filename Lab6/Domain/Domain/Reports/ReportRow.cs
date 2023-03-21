using Domain.Devices;
using Domain.Messages;
using RichEntity.Annotations;

namespace Domain.Reports;

#pragma warning disable SA1601
public partial class ReportRow : IEntity<Guid>
#pragma warning restore SA1601
{
    public ReportRow(Guid id, DeviceModel device, MessageCount count)
        : this(id)
    {
        Device = device;
        Count = count;
    }

    public virtual DeviceModel Device { get; protected init; }
    public virtual MessageCount Count { get; protected init; }
}
