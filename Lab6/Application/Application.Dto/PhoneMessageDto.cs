namespace Application.Dto;

public record PhoneMessageDto(
    Guid Id,
    Guid SenderDeviceId,
    Guid CatcherDeviceId,
    Guid SenderId,
    Guid CatcherId,
    DateTime CreationTime,
    string Body)
    : MessageDto(Id, SenderDeviceId, CatcherDeviceId, SenderId, CatcherId, CreationTime)
{ }