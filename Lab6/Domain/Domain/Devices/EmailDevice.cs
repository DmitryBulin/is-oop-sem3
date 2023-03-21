using Domain.Messages;

namespace Domain.Devices;

#pragma warning disable SA1601
public partial class EmailDevice : Device
#pragma warning restore SA1601
{
    public EmailDevice(Guid id, EmailLogin login)
        : base(id)
    {
        Login = login;
    }

    public virtual EmailLogin Login { get; protected init; }

    public void AddMessage(EmailMessage message) => base.AddMessage(message);
    public void RemoveMessage(EmailMessage message) => base.RemoveMessage(message);
}
