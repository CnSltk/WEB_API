using DeviceLibrary.Models;

namespace DeviceLibrary;

public class DeviceManager
{
    private const int MaxCapacity = 15;
    private readonly List<Device> _devices;

    public DeviceManager(IDeviceLoader loader, string filePath)
    {
        _devices = loader.LoadDevicesFromFile(filePath);
    }

    public List<Device> GetAll() => _devices;

    public void DisplayAllDevices()
    {
        foreach (var device in _devices)
        {
            Console.WriteLine(device);
        }
    }

    public Device? GetById(string id)
    {
        return _devices.Find(d => d.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    public bool DeleteById(string id)
    {
        var device = GetById(id);
        if (device == null) return false;

        _devices.Remove(device);
        return true;
    }

    public bool Add(Device device)
    {
        if (_devices.Count >= MaxCapacity)
            throw new InvalidOperationException("Device capacity reached (15 devices max).");

        _devices.Add(device);
        return true;
    }

    public bool Update(string id, Device newDevice)
    {
        var index = _devices.FindIndex(d => d.Id == id);
        if (index == -1) return false;

        _devices[index] = newDevice;
        return true;
    }

    public bool TurnOnDevice(string id)
    {
        var device = GetById(id);
        if (device == null) return false;

        try
        {
            device.TurnOn();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
            return false;
        }
    }

    public bool TurnOffDevice(string id)
    {
        var device = GetById(id);
        if (device == null) return false;

        device.TurnOff();
        return true;
    }

    public void SaveDevicesToFile(string path)
    {
        var lines = _devices.Select(d => d.ToString()); // Or ToFileFormat if you create one
        File.WriteAllLines(path, lines);
    }
}