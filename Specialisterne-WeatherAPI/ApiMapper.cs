using Microsoft.EntityFrameworkCore;

namespace Specialisterne_WeatherAPI;

public static class ApiMapper
{
    public static RouteGroupBuilder MapEndpoints<T>(this WebApplication app, DbContext db) where T : class
    {
        var routeName = typeof(T).Name.ToLower();
        var api = app.MapGroup($"/api/{routeName}");

        api.MapGet("/", () =>
                db.Set<T>().ToList())
            .WithName($"Get{typeof(T).Name}");

        api.MapGet("/one", () =>
            db.Set<T>().OrderBy(t => EF.Property<int>(t, "Id"))
                .FirstOrDefault() is { } item
                ? Results.Ok(item)
                : Results.NotFound())
            .WithName($"GetOne{typeof(T).Name}");

        api.MapGet("/{id}", (int id) =>
                db.Set<T>().Find(id) is { } item
                    ? Results.Ok(item)
                    : Results.NotFound())
            .WithName($"Get{typeof(T).Name}ById");

        List<string> columns = [
            "ReaderId",
            "DmiId"
        ];
        foreach (var c in columns)
        {
            try
            {
                if (db.Set<T>().Any(t => EF.Property<Guid?>(t, c).HasValue))
                {
                    api.MapGet($"/{c[0..(c.EndsWith("Id") ? -2 : c.Length)]}", (string id) =>
                        db.Set<T>()
                            .Where(t => EF.Property<Guid>(t, c).ToString() == id)
                            .FirstOrDefault() is { } t
                            ? Results.Ok(t)
                            : Results.NotFound())
                        .WithName($"Get{typeof(T).Name}By{c}");
                }
            }
            catch
            {
                Console.WriteLine($"{typeof(T).Name} has no property `{c}`");
            }
        }

        return api;
    }
}
