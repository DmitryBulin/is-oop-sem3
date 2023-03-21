namespace Domain.Devices;

public record DeviceModel(Guid Id)
{
    public static DeviceModel FromDevice(Device device)
        => new DeviceModel(device.Id);
}
