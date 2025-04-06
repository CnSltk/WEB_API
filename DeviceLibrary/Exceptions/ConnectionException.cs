namespace DeviceLibrary.Exceptions;

public class ConnectionException : Exception
{
    public ConnectionException(string deviceId)
        : base($"Device '{deviceId}' failed to connect to network.") { }
}