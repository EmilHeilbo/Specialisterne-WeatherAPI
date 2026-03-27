using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Specialisterne_WeatherAPI.DTOs;

namespace Specialisterne_WeatherAPI;

public static class ApiMapper
{
    public static RouteGroupBuilder MapEndpoints<T>(this WebApplication app, DbContext db) where T : class
    {
        var routeName = typeof(T).Name.ToLower();
        var api = app.MapGroup($"/api/{routeName}");
        
        api.MapGetAll<T>(db);

        api.MapGet("/one", async () =>
            await db.Set<T>().OrderBy(t => EF.Property<int>(t, "Id"))
                .FirstOrDefaultAsync() is { } item
                ? Results.Ok(item)
                : Results.NotFound())
            .WithName($"GetOne{typeof(T).Name}");

        api.MapGet("/{id}", async (int id) =>
                await db.Set<T>().FindAsync(id) is { } item
                    ? Results.Ok(item)
                    : Results.NotFound())
            .WithName($"Get{typeof(T).Name}ById");

        if (typeof(T) == typeof(Dmi))
        {
            api.MapGet("/foreign/{id}", async (string id) =>
                    await db.Set<Dmi>().FirstOrDefaultAsync(i =>
                        i.DmiId == Guid.Parse(id)) is { } dmi
                        ? Results.Ok(dmi)
                        : Results.NotFound())
                .WithName("GetDmiByForeignId");

            api.MapGet("/stations", async () =>
                    await db.Set<Dmi>().ToListAsync().ContinueWith(dmi =>
                        dmi.Result.DistinctBy(t => t.StationId)
                            .Select(t => t.StationId)))
                .WithName("GetStations");
        }
        else
            api.MapGet("/station/{readerId}", async (string readerId) =>
                    await db.Set<T>().FirstOrDefaultAsync(i =>
                        EF.Property<Guid>(i, "ReaderId") == Guid.Parse(readerId)) is { } item
                        ? Results.Ok(item)
                        : Results.NotFound())
                .WithName($"Get{typeof(T).Name}ByReaderId");

        return api;
    }
    
    private static void MapGetAll<T>(this RouteGroupBuilder api, DbContext db) where T : class
    {
        if (typeof(T) == typeof(Dmi))
            api.MapGet("/override", (
                [FromQuery] DateTime? fromDate,
                [FromQuery] DateTime? toDate,
                [FromQuery] int? maxResults,
                [FromQuery] int? stationId) => {
                var from = (fromDate ?? DateTime.MinValue).ToUniversalTime();
                var to = (toDate ?? DateTime.Now).ToUniversalTime();
                var max = maxResults ?? int.MaxValue;
                
                return Results.Ok(db.Set<Dmi>().Where(t =>
                    t.ObservedAt >= from && t.ObservedAt <= to
                    && (t.StationId == stationId || stationId == null)
                ).Take(max));

            }).WithName($"GetDmi");
        else
            api.MapGet("/", (
                [FromQuery] DateTime? fromDate,
                [FromQuery] DateTime? toDate,
                [FromQuery] int? maxResults) => {
                var from = (fromDate ?? DateTime.MinValue).ToUniversalTime();
                var to = (toDate ?? DateTime.Now).ToUniversalTime();
                var max = maxResults ?? int.MaxValue;
                    
                return Results.Ok(db.Set<T>().Where(t =>
                        EF.Property<DateTime>(t, "ObservedAt") >= from
                        && EF.Property<DateTime>(t, "ObservedAt") <= to
                    ).Take(max));
            }).WithName($"Get{typeof(T).Name}");
            
    }
}
