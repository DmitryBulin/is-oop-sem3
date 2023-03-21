namespace Application.Dto;

public record EmailDeviceDto(Guid Id, string Login)
    : DeviceDto(Id);