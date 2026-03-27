using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Specialisterne_WeatherAPI;
using Specialisterne_WeatherAPI.Context;
using Specialisterne_WeatherAPI.DTOs;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddDbContext<DmiDbContext>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

using var scope = app.Services.CreateScope();

var dmiDb = scope.ServiceProvider.GetRequiredService<DmiDbContext>(); 

var dmiApi = app.MapEndpoints<Dmi>(dmiDb);
dmiApi.MapGet("/dmi/{dmiId}", (string dmiId) =>
    dmiDb.Dmi.FirstOrDefault(i =>
        i.DmiId == Guid.Parse(dmiId)) is { } dmi
        ? Results.Ok(dmi)
        : Results.NotFound());

var bmeApi = app.MapEndpoints<Bme280>(dmiDb);
bmeApi.MapGet("/station/{readerId}", (string readerId) =>
    dmiDb.Bme280.FirstOrDefault(i =>
        i.ReaderId == Guid.Parse(readerId)) is { } bme
        ? Results.Ok(bme)
        : Results.NotFound());

var dsApi = app.MapEndpoints<Ds18B20>(dmiDb);
dsApi.MapGet("/station/{readerId}", (string readerId) =>
    dmiDb.Ds18B20.FirstOrDefault(i =>
        i.ReaderId == Guid.Parse(readerId)) is { } bme
        ? Results.Ok(bme)
        : Results.NotFound());

var scdApi = app.MapEndpoints<Scd41>(dmiDb);
scdApi.MapGet("/station/{readerId}", (string readerId) =>
    dmiDb.Scd41.FirstOrDefault(i =>
        i.ReaderId == Guid.Parse(readerId)) is { } bme
        ? Results.Ok(bme)
        : Results.NotFound());

app.Run();

[JsonSerializable(typeof(List<Dmi>))]
[JsonSerializable(typeof(List<Bme280>))]
[JsonSerializable(typeof(List<Ds18B20>))]
[JsonSerializable(typeof(List<Scd41>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
