using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Models;
using PW_Museo.Repositories;

namespace PW_Museo.Endpoints
{
    public static class GuidedVisitsEndpoints
    {
        public static void MapGuidedVisitsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/guidedvisits");

            group.MapGet("/", async (GuidedVisitsRepository repo) => Results.Ok(await repo.GetAllAsync()));
            group.MapGet("/{id:guid}", async (Guid id, GuidedVisitsRepository repo) =>
            {
                var item = await repo.GetByIdAsync(id);
                return item is not null ? Results.Ok(item) : Results.NotFound();
            });
            group.MapPost("/", async (Guided_Visit item, GuidedVisitsRepository repo) =>
            {
                item.Id = Guid.NewGuid();
                await repo.CreateAsync(item);
                return Results.Created($"/api/guidedvisits/{item.Id}", item);
            });
            group.MapPut("/{id:guid}", async (Guid id, Guided_Visit item, GuidedVisitsRepository repo) =>
            {
                if (id != item.Id) return Results.BadRequest();
                if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
                await repo.UpdateAsync(item);
                return Results.NoContent();
            });
            group.MapDelete("/{id:guid}", async (Guid id, GuidedVisitsRepository repo) =>
            {
                if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
                await repo.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}