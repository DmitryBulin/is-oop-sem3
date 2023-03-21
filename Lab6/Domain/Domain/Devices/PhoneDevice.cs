using Domain.Messages;

namespace Domain.Devices;

#pragma warning disable SA1601
public partial class PhoneDevice : Device
#pragma warning restore SA1601
{
    public PhoneDevice(Guid id, PhoneNumber number)
        : base(id)
    {
        Number = number;
    }

    public PhoneNumber Number { get; protected init; }

    public void AddMessage(PhoneMessage message) => base.AddMessage(message);
    public void RemoveMessage(PhoneMessage message) => base.RemoveMessage(message);
}
