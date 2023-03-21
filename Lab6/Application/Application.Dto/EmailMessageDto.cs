namespace Application.Dto;

public record EmailMessageDto(
    Guid Id,
    Guid SenderDeviceId,
    Guid CatcherDeviceId,
    Guid SenderId,
    Guid CatcherId,
    DateTime CreationTime,
    string Theme,
    string Body)
    : MessageDto(Id, SenderDeviceId, CatcherDeviceId, SenderId, CatcherId, CreationTime)
{ }