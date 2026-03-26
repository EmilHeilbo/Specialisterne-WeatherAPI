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
app.MapEndpoints<Dmi>(dmiDb);
app.MapEndpoints<Bme280>(dmiDb);
app.MapEndpoints<Ds18B20>(dmiDb);
app.MapEndpoints<Scd41>(dmiDb);

app.Run();

[JsonSerializable(typeof(List<Dmi>))]
[JsonSerializable(typeof(List<Bme280>))]
[JsonSerializable(typeof(List<Ds18B20>))]
[JsonSerializable(typeof(List<Scd41>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
