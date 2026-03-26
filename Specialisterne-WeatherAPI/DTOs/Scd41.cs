using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Specialisterne_WeatherAPI.DTOs;

[Table("SCD41"), PrimaryKey("Id")]
public record Scd41(
    [property: Column("SCD41_id")] int Id,
    [property: Column("reader_id")] Guid ReaderId,
    [property: Column("co2")] int Co2,
    [property: Column("humidity"), Precision(20, 13)] decimal Humidity,
    [property: Column("temperature"), Precision(20, 13)] decimal Temperature,
    [property: Column("observed_at")] DateTime ObservedAt,
    [property: Column("pulled_at")] DateTime PulledAt
);
