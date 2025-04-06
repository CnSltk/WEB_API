namespace DeviceLibrary.Exceptions;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException(string deviceId)
        : base($"Device '{deviceId}' has insufficient battery to turn on.") { }
}