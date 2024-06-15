using Azure.Storage.Blobs;
using ExampleUseAzureBlobStorage.Services.Files;
using ExampleUseAzureBlobStorage.Services.Files.Interfaces;
using ExampleUseAzureBlobStorage.Services.Files.Models;
using ExampleUseAzureBlobStorage.Services.Files.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<FileServiceOptions>("FileService");

var blobConnectionString = builder.Configuration["FileService:ConnectionString"];
builder.Services.AddSingleton(_ => new BlobServiceClient(blobConnectionString));
builder.Services.AddSingleton<IFileService, FileService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/upload", async (IFormFile form, IFileService fileService) =>
{
    using var stream = form.OpenReadStream();

    var request = new FileRequest(stream, form.ContentType);
    var fileId = await fileService.UploadAsync(request);
    return Results.Ok(fileId);
});

app.MapPost("/download", async (Guid fileId, IFileService fileService) => {

    var response = await fileService.DownloadAsync(fileId);

    return Results.File(response.Stream, response.ContentType);
});

app.Run();