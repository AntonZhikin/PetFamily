using System.Net.Mime;
using Amazon.S3;
using Amazon.S3.Model;
using FilesService.Core;
using FilesService.Jobs;
using FilesService.MongoDataAccess;
using Hangfire;

namespace FilesService.Features;

public static class CompleteMultipartUpload
{
     private record PartETagInfo(int PartNumber, string ETag);
     
     private record CompleteMultipartRequest(string UploadId, List<PartETagInfo> Parts);
     
     public sealed class EndPoint : IEndpoint
     {
          public void MapEndpoint(IEndpointRouteBuilder app)
          {
               app.MapPost("files/{key}/complete-multipart", Handler);
          }
     }

     private static async Task<IResult> Handler(
          string key,
          CompleteMultipartRequest request, 
          IFileRepository fileRepository,
          IAmazonS3 s3Client,   
          CancellationToken cancellationToken)
     {
          try
          {
               var fileId = Guid.NewGuid(); 
               
               var enqueueAt = TimeSpan.FromHours(24);
               var jobId = BackgroundJob.Schedule<ConfirmConsistencyJob>(j =>
                    j.Execute(fileId, key, "main-bucket", cancellationToken), enqueueAt);
               
               var completeRequest = new CompleteMultipartUploadRequest()
               {
                    BucketName = "main-bucket",
                    Key = key,
                    UploadId = request.UploadId,
                    PartETags = request.Parts
                         .Select(part => new PartETag(part.PartNumber, part.ETag)).ToList()
               };
               
               var response = await s3Client
                    .CompleteMultipartUploadAsync(completeRequest, cancellationToken);

               var metaDataRequest = new GetObjectMetadataRequest()
               {
                    BucketName = "main-bucket",
                    Key = key,
               };
               var metaData = await s3Client.GetObjectMetadataAsync(metaDataRequest, cancellationToken);

               var fileData = new FileData
               {
                    Id = fileId,
                    StoragePath = key,
                    Size = metaData.Headers.ContentLength,
                    ContentType = metaData.Headers.ContentType,
                    UploadDate = DateTime.UtcNow,
                    BucketName = "main-bucket",
               };
               
               await fileRepository.Add(fileData, cancellationToken);
               
               BackgroundJob.Delete(jobId);
               
               return Results.Ok(new
               {
                    Id = key,
                    Size = response.ContentLength, 
                    ContentType = response.Location
               });
          }
          catch (AmazonS3Exception e)
          {
               return Results.BadRequest(e.Message); 
          }
     }
}