using DeviceLibrary.Models;

namespace DeviceLibrary;

public class DataLoader : IDeviceLoader
{
    private readonly IDeviceParser _deviceParser;

    public DataLoader(IDeviceParser deviceParser)
    {
        _deviceParser = deviceParser;
    }

    public List<Device> LoadDevicesFromFile(string filePath)
    {
        var devices = new List<Device>();

        foreach (var line in File.ReadLines(filePath))
        {
            try
            {
                var device = _deviceParser.Parse(line);
                devices.Add(device);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARNING] Could not parse line: '{line}' → {ex.Message}");
            }
        }

        return devices;
    }
}