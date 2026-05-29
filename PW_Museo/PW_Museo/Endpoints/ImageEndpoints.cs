using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PW_Museo.Services;

namespace PW_Museo.Endpoints;

public static class ImagesEndpoints
{
    public static void MapImagesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/images");

        group.MapPost("/upload", async (IFormFile file, BlobService blob, IImagesDA imagesDA) =>
        {
            using var stream = file.OpenReadStream();
            var url = await blob.UploadAsync(stream, file.FileName, file.ContentType);
            var id = await imagesDA.CreateAsync(url);
            return Results.Ok(new { id, url });
        }).DisableAntiforgery();

        group.MapGet("/{id:guid}", async (Guid id, IImagesDA imagesDA) =>
        {
            var image = await imagesDA.GetByIdAsync(id);
            return image is not null ? Results.Ok(image) : Results.NotFound();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IImagesDA imagesDA, BlobService blob) =>
        {
            var image = await imagesDA.GetByIdAsync(id);
            if (image is null) return Results.NotFound();
            await blob.DeleteAsync(image.Src);
            await imagesDA.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}