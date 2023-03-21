using Domain.Devices;
using Domain.Users;

namespace Domain.Messages;

public partial class PhoneMessage : Message
{
    public PhoneMessage(
        Guid id,
        Device senderDevice,
        Device catcherDevice,
        Worker sender,
        Worker catcher,
        DateTime creationTime,
        MessageData body)
        : base(id, senderDevice, catcherDevice, sender, catcher, creationTime)
    {
        Body = body;
    }

    public virtual MessageData Body { get; init; }
}
