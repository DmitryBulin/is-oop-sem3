using Domain.Devices;
using Domain.Users;

namespace Domain.Messages;

public partial class EmailMessage : Message
{
    public EmailMessage(
        Guid id,
        Device senderDevice,
        Device catcherDevice,
        Worker sender,
        Worker catcher,
        DateTime creationTime,
        MessageData theme,
        MessageData body)
        : base(id, senderDevice, catcherDevice, sender, catcher, creationTime)
    {
        Theme = theme;
        Body = body;
    }

    public virtual MessageData Theme { get; init; }
    public virtual MessageData Body { get; init; }
}
