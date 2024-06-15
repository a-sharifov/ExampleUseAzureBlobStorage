namespace ExampleUseAzureBlobStorage.AppHost.Resources;

public class BlobStorageResource(string name) : ContainerResource(name)
{
    internal const string TcpEndpointName = "tcp";
}
