using DeviceLibrary;
using DeviceLibrary.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Device API", Version = "v1" });
});
builder.Services.AddSingleton<IDeviceParser, DeviceParser>();
builder.Services.AddSingleton<IDeviceLoader, DataLoader>();
builder.Services.AddSingleton<DeviceManager>(sp =>
{
    var parser = sp.GetRequiredService<IDeviceParser>();
    var loader = new DataLoader(parser);
    return new DeviceManager(loader, "input.txt");
});
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// GET /devices
// Retrieve a list of all devices
app.MapGet("/devices", (DeviceManager manager) =>
{
    var devices = manager.GetAllDevices();
    return Results.Ok(devices);
});
// GET /devices/{id}
// Retrieve details of a device by its ID
app.MapGet("/devices/{id}", (string id, DeviceManager manager) =>
{
    var device = manager.GetDeviceById(id);
    return device != null ? Results.Ok(device) : Results.NotFound();
});
// POST /devices
// Add a new device (data is sent in request body as JSON)
app.MapPost("/devices", async (DeviceManager manager, HttpContext context) =>
{
    var device = await context.Request.ReadFromJsonAsync<Device>();
    if (device is null || string.IsNullOrWhiteSpace(device.Id))
        return Results.BadRequest("Invalid device data.");

    manager.AddDevice(device);
    return Results.Created($"/devices/{device.Id}", device);
});
// PUT /devices/{id}
// Update the name of an existing device
app.MapPut("/devices/{id}", async (string id, HttpContext context, DeviceManager manager) =>
{
    var updatedDevice = await context.Request.ReadFromJsonAsync<Device>();
    if (updatedDevice is null || string.IsNullOrWhiteSpace(updatedDevice.Name))
        return Results.BadRequest("Invalid input.");

    var success = manager.UpdateDevice(id, updatedDevice.Name);
    return success ? Results.Ok(updatedDevice) : Results.NotFound();
});
// DELETE /devices/{id}
// Delete a device by ID
app.MapDelete("/devices/{id}", (string id, DeviceManager manager) =>
{
    var success = manager.DeleteDevice(id);
    return success ? Results.Ok($"Device {id} deleted.") : Results.NotFound();
});

app.Run();
