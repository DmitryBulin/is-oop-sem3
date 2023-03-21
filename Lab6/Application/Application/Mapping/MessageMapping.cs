using Application.Dto;
using Domain.Messages;

namespace Application.Mapping;

public static class MessageMapping
{
    public static EmailMessageDto AsDto(this EmailMessage message)
        => new EmailMessageDto(
            message.Id,
            message.SenderDevice.Id,
            message.CatcherDevice.Id,
            message.Sender.Id,
            message.Catcher.Id,
            message.CreationTime,
            message.Theme.Value,
            message.Body.Value);

    public static PhoneMessageDto AsDto(this PhoneMessage message)
        => new PhoneMessageDto(
            message.Id,
            message.SenderDevice.Id,
            message.CatcherDevice.Id,
            message.Sender.Id,
            message.Catcher.Id,
            message.CreationTime,
            message.Body.Value);
}
