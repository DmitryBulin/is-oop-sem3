using Application.Dto;
using Domain.Devices;

namespace Application.Mapping;

public static class DeviceMapping
{
    public static EmailDeviceDto AsDto(this EmailDevice device)
        => new EmailDeviceDto(device.Id, device.Login.Value);

    public static PhoneDeviceDto AsDto(this PhoneDevice device)
        => new PhoneDeviceDto(device.Id, device.Number.Value);
}
