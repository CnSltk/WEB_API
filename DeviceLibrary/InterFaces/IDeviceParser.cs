using DeviceLibrary.Models;

namespace DeviceLibrary;

public interface IDeviceParser
{
    Device Parse(string line);
}