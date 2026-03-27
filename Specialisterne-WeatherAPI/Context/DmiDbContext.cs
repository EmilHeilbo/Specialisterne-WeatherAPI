using Microsoft.EntityFrameworkCore;
using Specialisterne_WeatherAPI.DTOs;

namespace Specialisterne_WeatherAPI.Context;

public sealed class DmiDbContext(DbContextOptions<DmiDbContext> options) : DbContext(options)
{
    public DbSet<Bme280> Bme280 { get; set; }
    public DbSet<Dmi> Dmi { get; set; }
    public DbSet<Ds18B20> Ds18B20 { get; set; }
    public DbSet<Scd41> Scd41 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "weatherman";
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Some_Overly_Complicated_Password,_I_Guess?";
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "herodot_etl";

        optionsBuilder.UseNpgsql($"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}");
    }

    [Obsolete("DB Context is read-only",true)]
    #pragma warning disable CS0809
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    [Obsolete("DB Context is read-only",true)]
    #pragma warning disable CS0809
    public override int SaveChanges(bool acceptAll)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    [Obsolete("DB Context is read-only",true)]
    #pragma warning disable CS0809
    public override Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    [Obsolete("DB Context is read-only",true)]
    #pragma warning disable CS0809
    public override Task<int> SaveChangesAsync(bool acceptAll, CancellationToken token = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}
