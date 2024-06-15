namespace ExampleUseAzureBlobStorage.Services.Files.Models;

public sealed record FileResponse(Stream Stream, string ContentType);