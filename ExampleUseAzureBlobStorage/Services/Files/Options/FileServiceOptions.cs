namespace ExampleUseAzureBlobStorage.Services.Files.Options;

public sealed class FileServiceOptions
{
    public string ConnectionString { get; set; } = null!;
    public string ContainerName { get; set; } = null!;
}
