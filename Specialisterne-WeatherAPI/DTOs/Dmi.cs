using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Specialisterne_WeatherAPI.DTOs;

[Table("DMI"), PrimaryKey("Id")]
public record Dmi(
    [property: Column("DMI_id")] int Id,
    [property: Column("dmi_id")] Guid DmiId,
    [property: Column("parameter_id"), MaxLength(50)] string ParameterId,
    [property: Column("value")] double Value,
    [property: Column("observed_at")] DateTime ObservedAt,
    [property: Column("pulled_at")] DateTime PulledAt,
    [property: Column("station_id")] int StationId
);