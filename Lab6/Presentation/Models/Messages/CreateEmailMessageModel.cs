namespace Presentation.Models.Messages;

public record CreateEmailMessageModel(
    Guid SenderDeviceId,
    Guid CatcherDeviceId,
    Guid SenderId,
    Guid CatcherId,
    DateTime CreationTime,
    string Theme,
    string Body);
