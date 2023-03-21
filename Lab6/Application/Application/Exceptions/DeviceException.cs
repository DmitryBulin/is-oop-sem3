namespace Application.Exceptions;

public class DeviceException : ApplicationException
{
    private DeviceException(string? message)
        : base(message) { }

    public static DeviceException WrongDeviceType<TExpect>()
        => new DeviceException($"Expected device {typeof(TExpect)}");
}
