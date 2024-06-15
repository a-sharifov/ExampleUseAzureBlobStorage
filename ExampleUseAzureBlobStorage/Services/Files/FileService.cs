using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ExampleUseAzureBlobStorage.Services.Files.Interfaces;
using ExampleUseAzureBlobStorage.Services.Files.Models;
using ExampleUseAzureBlobStorage.Services.Files.Options;
using Microsoft.Extensions.Options;

namespace ExampleUseAzureBlobStorage.Services.Files;

public class FileService(
    BlobServiceClient blobService, 
    IOptions<FileServiceOptions> options) 
    : IFileService
{
    private readonly BlobServiceClient _blobService = blobService;
    private readonly FileServiceOptions _options = options.Value;

    public async Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        var container = _blobService.GetBlobContainerClient(_options.ContainerName);

        var blobClient = container.GetBlobClient(fileId.ToString());

        var response = await blobClient.DownloadContentAsync(cancellationToken);

        return new FileResponse(
            response.Value.Content.ToStream(), 
            response.Value.Details.ContentType);
    }

    public async Task<Guid> UploadAsync(FileRequest request, CancellationToken cancellationToken = default)
    {
        var container = _blobService.GetBlobContainerClient(_options.ContainerName);
        var fileId = Guid.NewGuid();
        var blobClient = container.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(
            request.Stream, 
            new BlobHttpHeaders { ContentType = request.ContentType }, 
            cancellationToken: cancellationToken);

        return fileId;
    }
}
