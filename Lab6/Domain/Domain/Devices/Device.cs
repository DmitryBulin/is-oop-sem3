using Domain.Common.Exceptions;
using Domain.Messages;
using RichEntity.Annotations;

namespace Domain.Devices;

#pragma warning disable SA1601
public abstract partial class Device : IEntity<Guid>
#pragma warning restore SA1601
{
    private readonly HashSet<Message> _messages = new HashSet<Message>();

    public virtual IReadOnlyCollection<Message> Messages => _messages;

    protected void AddMessage(Message message)
    {
        if (_messages.Add(message) is false)
        {
            throw DeviceException.MessageAlreadyExists(Id, message.Id);
        }
    }

    protected void RemoveMessage(Message message)
    {
        if (_messages.Remove(message) is false)
        {
            throw DeviceException.MessageDoesNotExist(Id, message.Id);
        }
    }
}
