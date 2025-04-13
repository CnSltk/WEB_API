using DeviceLibrary;
using DeviceLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeviceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly DeviceManager _deviceManager;

    public DeviceController(DeviceManager deviceManager)
    {
        _deviceManager = deviceManager;
    }

    [HttpGet]
    public IResult GetAllDevices()
    {
        return Results.Ok(_deviceManager.GetAllDevices());
    }

    [HttpGet("{id}")]
    public IResult GetDeviceById(string id)
    {
        var device = _deviceManager.GetDeviceById(id);
        return device == null ? Results.NotFound() : Results.Ok(device);
    }

    [HttpPost]
    public IResult AddDevice([FromBody] Device device)
    {
        _deviceManager.AddDevice(device);
        return Results.Created($"/api/device/{device.Id}", device);
    }

    [HttpPut("{id}")]
    public IResult UpdateDevice(string id, [FromBody] string newName)
    {
        var updated = _deviceManager.UpdateDevice(id, newName);
        return updated ? Results.NoContent() : Results.NotFound();
    }

    [HttpDelete("{id}")]
    public IResult DeleteDevice(string id)
    {
        var deleted = _deviceManager.DeleteDevice(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}