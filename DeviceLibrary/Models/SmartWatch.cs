using DeviceLibrary.Exceptions;

namespace DeviceLibrary.Models;

public class Smartwatch : Device
{
    public int Battery { get; private set; }

    public Smartwatch(string id, string name, int battery) : base(id, name)
    {
        if (battery < 0 || battery > 100)
            throw new ArgumentException("Battery must be between 0 and 100.");

        Battery = battery;
    }

    public override void TurnOn()
    {
        if (Battery < 11)
            throw new EmptyBatteryException(Id);

        if (Battery < 20)
        {
            IPowerNotifier.NotifyLowPower(this);
        }

        Battery -= 10;
        IsOn = true;
    }
}