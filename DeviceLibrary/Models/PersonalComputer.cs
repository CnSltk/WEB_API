using DeviceLibrary.Exceptions;

namespace DeviceLibrary.Models;

public class PersonalComputer : Device
{
    public string OperatingSystem { get; }

    public PersonalComputer(string id, string name, string os) : base(id, name)
    {
        OperatingSystem = os;
    }

    public override void TurnOn()
    {
        if (string.IsNullOrWhiteSpace(OperatingSystem))
            throw new EmptySystemException(Id);

        IsOn = true;
    }
}