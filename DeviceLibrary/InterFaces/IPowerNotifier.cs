using DeviceLibrary.Models;

namespace DeviceLibrary;

public static class IPowerNotifier
{
    public static void NotifyLowPower(Device device)
    {
        Console.WriteLine($"⚠️ Low battery warning for {device.Id}: Battery is below 20%");
    }
}