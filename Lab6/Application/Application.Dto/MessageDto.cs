namespace Application.Dto;

public record MessageDto(
    Guid Id,
    Guid SenderDeviceId,
    Guid CatcherDeviceId,
    Guid SenderId,
    Guid CatcherId,
    DateTime CreationTime);
