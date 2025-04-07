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
    public ActionResult<IEnumerable<Device>> GetAllDevices()
    {
        return Ok(_deviceManager.GetAllDevices());
    }

    [HttpGet("{id}")]
    public ActionResult<Device> GetDeviceById(string id)
    {
        var device = _deviceManager.GetDeviceById(id);
        return device == null ? NotFound() : Ok(device);
    }

    [HttpPost]
    public IActionResult AddDevice([FromBody] Device device)
    {
        _deviceManager.AddDevice(device);
        return CreatedAtAction(nameof(GetDeviceById), new { id = device.Id }, device);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDevice(string id, [FromBody] string newName)
    {
        var updated = _deviceManager.UpdateDevice(id, newName);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDevice(string id)
    {
        var deleted = _deviceManager.DeleteDevice(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("{id}/on")]
    public IActionResult TurnOn(string id)
    {
        _deviceManager.TurnOnDevice(id);
        return NoContent();
    }

    [HttpPost("{id}/off")]
    public IActionResult TurnOff(string id)
    {
        _deviceManager.TurnOffDevice(id);
        return NoContent();
    }
}