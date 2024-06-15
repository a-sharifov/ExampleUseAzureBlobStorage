using ExampleUseAzureBlobStorage.AppHost.Resources;
using Microsoft.Extensions.Azure;
using System.Reflection.Metadata;

var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.add("storage").RunAsEmulator(container =>
{
    container.WithDataBindMount();
});

var blobs = storage.AddBlobs("blobs");

builder.AddProject<Projects.ExampleUseAzureBlobStorage_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(blobs); 

builder.Build().Run();
