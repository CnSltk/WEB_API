using System.Text.RegularExpressions;
using DeviceLibrary.Exceptions;

namespace DeviceLibrary.Models;

public class EmbeddedDevice : Device
{
    public string IpAddress { get; }
    public string NetworkName { get; }

    public EmbeddedDevice(string id, string name, string ip, string network) : base(id, name)
    {
        if (!Regex.IsMatch(ip, @"^(\d{1,3}\.){3}\d{1,3}$"))
            throw new ArgumentException("Invalid IP address format.");

        IpAddress = ip;
        NetworkName = network;
    }

    public override void TurnOn()
    {
        Connect();
        IsOn = true;
    }

    private void Connect()
    {
        if (!NetworkName.Contains("MD Ltd."))
            throw new ConnectionException(Id);
    }
}