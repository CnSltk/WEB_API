namespace DeviceLibrary.Exceptions;

public class EmptySystemException : Exception
{
    public EmptySystemException(string deviceId)
        : base($"Device '{deviceId}' has no operating system defined.") { }
}