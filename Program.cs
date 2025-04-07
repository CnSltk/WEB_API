using DeviceLibrary;
using DeviceLibrary.Models;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Set up dependencies
IDeviceParser parser = new DeviceParser();
IDeviceLoader loader = new DataLoader(parser);
DeviceManager manager = new DeviceManager(loader, "input.txt");

// Get all devices
app.MapGet("/devices", () =>
{
    return Results.Ok(manager.GetAll());
});

// Get device by ID
app.MapGet("/devices/{id}", (string id) =>
{
    var device = manager.GetById(id);
    return device is null ? Results.NotFound() : Results.Ok(device);
});

// Add a new device
app.MapPost("/devices", (Device device) =>
{
    try
    {
        manager.Add(device);
        return Results.Created($"/devices/{device.Id}", device);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// Update device
app.MapPut("/devices/{id}", (string id, Device updated) =>
{
    if (!manager.Update(id, updated))
        return Results.NotFound();
    return Results.Ok();
});

// Delete device
app.MapDelete("/devices/{id}", (string id) =>
{
    if (!manager.DeleteById(id))
        return Results.NotFound();
    return Results.Ok();
});

// Turn ON a device
app.MapPost("/devices/{id}/turnon", (string id) =>
{
    return manager.TurnOnDevice(id) ? Results.Ok() : Results.BadRequest("Could not turn on device.");
});

// Turn OFF a device
app.MapPost("/devices/{id}/turnoff", (string id) =>
{
    return manager.TurnOffDevice(id) ? Results.Ok() : Results.BadRequest("Could not turn off device.");
});

// Save all devices to a file
app.MapPost("/devices/save", () =>
{
    manager.SaveDevicesToFile("output.txt");
    return Results.Ok("Devices saved to output.txt");
});

app.Run();