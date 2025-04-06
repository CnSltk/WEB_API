using DeviceLibrary.Models;

namespace DeviceLibrary;

public interface IDeviceLoader
{
    List<Device> LoadDevicesFromFile(string filePath);
}