namespace Presentation.Models.Messages;

public record CreatePhoneMessageModel(
    Guid SenderDeviceId,
    Guid CatcherDeviceId,
    Guid SenderId,
    Guid CatcherId,
    DateTime CreationTime,
    string Body);
