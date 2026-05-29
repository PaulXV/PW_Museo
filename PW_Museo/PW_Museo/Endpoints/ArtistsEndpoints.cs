using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Models;
using PW_Museo.Services;

namespace PW_Museo.Endpoints;

public static class ArtistsEndpoints
{
    public static void MapArtistsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/artists");

        group.MapGet("/", async (IArtistsDA repo) => Results.Ok(await repo.GetAllAsync()));
        group.MapGet("/{id:guid}", async (Guid id, IArtistsDA repo) =>
        {
            var item = await repo.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        });
        group.MapPost("/", async (Artist item, IArtistsDA repo) =>
        {
            item.Id = Guid.NewGuid();
            await repo.CreateAsync(item);
            return Results.Created($"/api/artists/{item.Id}", item);
        });
        group.MapPut("/{id:guid}", async (Guid id, Artist item, IArtistsDA repo) =>
        {
            if (id != item.Id) return Results.BadRequest();
            if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
            await repo.UpdateAsync(item);
            return Results.NoContent();
        });
        group.MapDelete("/{id:guid}", async (Guid id, IArtistsDA repo) =>
        {
            if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
            await repo.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}