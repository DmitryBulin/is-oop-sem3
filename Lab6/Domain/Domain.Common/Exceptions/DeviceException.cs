namespace Domain.Common.Exceptions;

public class DeviceException : DomainException
{
    private DeviceException(string? message)
        : base(message) { }

    public static DeviceException MessageAlreadyExists(Guid deviceId, Guid messageId)
        => new DeviceException($"Message {messageId} already exist in device {deviceId}");

    public static DeviceException MessageDoesNotExist(Guid deviceId, Guid messageId)
        => new DeviceException($"Message {messageId} does not exist in device {deviceId}");
}
