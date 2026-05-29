using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Models;
using PW_Museo.Services;

namespace PW_Museo.Endpoints;

public static class OperasEndpoints
{
    public static void MapOperasEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/operas");

        group.MapGet("/", async (IOperasDA repo) => Results.Ok(await repo.GetAllAsync()));
        group.MapGet("/all-details", async (IOperasDA repo) => Results.Ok(await repo.GetAllDetailAsync()));
        group.MapGet("/{id:guid}", async (Guid id, IOperasDA repo) =>
        {
            var item = await repo.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        });
        group.MapPost("/", async (Opera item, IOperasDA repo) =>
        {
            item.Id = Guid.NewGuid();
            await repo.CreateAsync(item);
            return Results.Created($"/api/operas/{item.Id}", item);
        });
        group.MapPut("/{id:guid}", async (Guid id, Opera item, IOperasDA repo) =>
        {
            if (id != item.Id) return Results.BadRequest();
            if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
            await repo.UpdateAsync(item);
            return Results.NoContent();
        });
        group.MapDelete("/{id:guid}", async (Guid id, IOperasDA repo) =>
        {
            if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
            await repo.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}