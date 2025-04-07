using System.Collections.Generic;
using DeviceLibrary.Models;

public interface IDeviceService
{
    IEnumerable<Device> GetAllDevices();
}