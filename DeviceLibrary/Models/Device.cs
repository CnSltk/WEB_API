namespace DeviceLibrary.Models;

public abstract class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsOn { get; protected set; }

    protected Device(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public abstract void TurnOn();
    public virtual void TurnOff() => IsOn = false;

    public override string ToString()
    {
        return $"[{GetType().Name}] ID: {Id}, Name: {Name}, On: {IsOn}";
    }
}