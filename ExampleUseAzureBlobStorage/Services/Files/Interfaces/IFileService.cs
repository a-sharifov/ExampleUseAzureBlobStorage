using ExampleUseAzureBlobStorage.Services.Files.Models;

namespace ExampleUseAzureBlobStorage.Services.Files.Interfaces;

public interface IFileService
{
    Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);
    Task<Guid> UploadAsync(FileRequest request, CancellationToken cancellationToken = default);
}
