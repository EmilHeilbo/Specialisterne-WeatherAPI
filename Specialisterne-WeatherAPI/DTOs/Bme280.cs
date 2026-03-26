using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Specialisterne_WeatherAPI.DTOs;

[Table("BME280"), PrimaryKey("Id")]
public record Bme280(
    [property: Column("BME280_id")] int Id,
    [property: Column("reader_id")] Guid ReaderId,
    [property: Column("location"), MaxLength(7)] string Location,
    [property: Column("humidity"), Precision(20, 13)] decimal Humidity,
    [property: Column("temperature"), Precision(20, 13)] decimal Temperature,
    [property: Column("pressure"), Precision(20, 13)] decimal Pressure,
    [property: Column("observed_at")] DateTime ObservedAt,
    [property: Column("pulled_at")] DateTime PulledAt
);
