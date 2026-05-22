using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Models;
using PW_Museo.Repositories;

namespace PW_Museo.Endpoints
{
    public static class TicketsEndpoints
    {
        public static void MapTicketsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/tickets");

            group.MapGet("/", async (TicketsRepository repo) => Results.Ok(await repo.GetAllAsync()));
            group.MapGet("/{id:guid}", async (Guid id, TicketsRepository repo) =>
            {
                var item = await repo.GetByIdAsync(id);
                return item is not null ? Results.Ok(item) : Results.NotFound();
            });
            group.MapPost("/", async (Ticket item, TicketsRepository repo) =>
            {
                item.Id = Guid.NewGuid();
                await repo.CreateAsync(item);
                return Results.Created($"/api/tickets/{item.Id}", item);
            });
            group.MapPut("/{id:guid}", async (Guid id, Ticket item, TicketsRepository repo) =>
            {
                if (id != item.Id) return Results.BadRequest();
                if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
                await repo.UpdateAsync(item);
                return Results.NoContent();
            });
            group.MapDelete("/{id:guid}", async (Guid id, TicketsRepository repo) =>
            {
                if (await repo.GetByIdAsync(id) is null) return Results.NotFound();
                await repo.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}