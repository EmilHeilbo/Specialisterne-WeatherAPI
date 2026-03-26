using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Specialisterne_WeatherAPI.DTOs;

[Table("DS18B20"), PrimaryKey("Id")]
public record Ds18B20(
    [property: Column("DS18B20_id")] int Id,
    [property: Column("reader_id")] Guid ReaderId,
    [property: Column("location"), MaxLength(7)] string Location,
    [property: Column("temperature"), Precision(20, 13)] decimal Temperature,
    [property: Column("observed_at")] DateTime ObservedAt,
    [property: Column("pulled_at")] DateTime PulledAt
);
