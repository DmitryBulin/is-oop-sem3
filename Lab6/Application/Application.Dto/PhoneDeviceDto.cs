namespace Application.Dto;

public record PhoneDeviceDto(Guid Id, string Number)
    : DeviceDto(Id);