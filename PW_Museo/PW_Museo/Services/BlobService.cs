using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace PW_Museo.Services;

public class BlobService
{
    private readonly BlobContainerClient _container;

    public BlobService(IConfiguration configuration)
    {
        var connStr = configuration["AzureBlobStorage:ConnectionString"]!;
        var containerName = configuration["AzureBlobStorage:ContainerName"]!;
        _container = new BlobContainerClient(connStr, containerName);
        _container.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var blobName = $"{Guid.NewGuid()}_{fileName}";
        var blobClient = _container.GetBlobClient(blobName);
        await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
        return blobClient.Uri.ToString();
    }

    public async Task DeleteAsync(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        var blobName = uri.Segments.Last();
        var blobClient = _container.GetBlobClient(blobName);
        await blobClient.DeleteIfExistsAsync();
    }
}