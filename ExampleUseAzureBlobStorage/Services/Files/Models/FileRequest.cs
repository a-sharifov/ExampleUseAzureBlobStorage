namespace ExampleUseAzureBlobStorage.Services.Files.Models;

public sealed record FileRequest(Stream Stream, string ContentType);
