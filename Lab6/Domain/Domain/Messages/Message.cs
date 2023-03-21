using Domain.Devices;
using Domain.Users;
using RichEntity.Annotations;

namespace Domain.Messages;

public abstract partial class Message : IEntity<Guid>
{
    public Message(Guid id, Device senderDevice, Device catcherDevice, Worker sender, Worker catcher, DateTime creationTime)
        : this(id)
    {
        SenderDevice = senderDevice;
        CatcherDevice = catcherDevice;
        Sender = sender;
        Catcher = catcher;
        CreationTime = creationTime;
    }

    public virtual Device SenderDevice { get; protected init; }
    public virtual Device CatcherDevice { get; protected init; }
    public virtual Worker Sender { get; protected init; }
    public virtual Worker Catcher { get; protected init; }
    public virtual DateTime CreationTime { get; protected init; }
    public virtual MessageState State { get; protected set; } = MessageState.Created;

    public virtual void Created() => State = MessageState.Created;
    public virtual void Recieved() => State = MessageState.Recieved;
    public virtual void Handled() => State = MessageState.Handled;
}

public enum MessageState
{
    Created,
    Recieved,
    Handled,
}
