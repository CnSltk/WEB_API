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

app.MapGet("/devices", (DeviceManager manager) =>
{
    var devices = manager.GetAllDevices();
    return Results.Ok(devices);
});

app.MapGet("/devices/{id}", (string id, DeviceManager manager) =>
{
    var device = manager.GetDeviceById(id);
    return device != null ? Results.Ok(device) : Results.NotFound();
});

app.MapPost("/devices/{id}/on", (string id, DeviceManager manager) =>
{
    manager.TurnOnDevice(id);
    return Results.Ok();
});

app.MapPost("/devices/{id}/off", (string id, DeviceManager manager) =>
{
    manager.TurnOffDevice(id);
    return Results.Ok();
});
app.MapPost("/devices", async (DeviceManager manager, HttpContext context) =>
{
    var device = await context.Request.ReadFromJsonAsync<Device>();
    if (device == null)
        return Results.BadRequest("Invalid device data.");

    manager.AddDevice(device);
    return Results.Created($"/devices/{device.Id}", device);
});
app.MapPut("/devices/{id}", (string id, Device updatedDevice, DeviceManager manager) =>
{
    var success = manager.UpdateDevice(id, updatedDevice.Name);
    return success ? Results.Ok(updatedDevice) : Results.NotFound();
});
app.MapDelete("/devices/{id}", (string id, DeviceManager manager) =>
{
    var success = manager.DeleteDevice(id);
    return success ? Results.Ok($"Device {id} deleted.") : Results.NotFound();
});



app.Run();