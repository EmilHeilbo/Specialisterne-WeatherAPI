using Microsoft.EntityFrameworkCore;

namespace Specialisterne_WeatherAPI;

public static class ApiMapper
{
    public static void MapEndpoints<T>(this WebApplication app, DbContext db) where T : class
    {
        var routeName = typeof(T).Name.ToLower();
        var api = app.MapGroup($"/api/{routeName}");
        
        api.MapGet("/", () => 
                db.Set<T>().ToList())
            .WithName($"Get{typeof(T).Name}");

        api.MapGet("/one", () =>
            db.Set<T>().FirstOrDefault() is { } item 
                ? Results.Ok(item)
                : Results.NotFound())
            .WithName($"GetOne{typeof(T).Name}");
        
        api.MapGet("/{id}", (int id) =>
                db.Set<T>().Find(id) is { } item 
                    ? Results.Ok(item) 
                    : Results.NotFound())
            .WithName($"Get{typeof(T).Name}ById");
    }
}