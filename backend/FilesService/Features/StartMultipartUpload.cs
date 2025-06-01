using Amazon.S3;
using Amazon.S3.Model;

namespace FilesService.Features;

public static class StartMultipartUpload
{
     private record StartMultipartUploadRequest(string FileName, string ContentType, long Size);
     
     public sealed class EndPoint : IEndpoint
     {
          public void MapEndpoint(IEndpointRouteBuilder app)
          {
               app.MapPost("files/multipart", Handler);
          }
     }

     private static async Task<IResult> Handler(
          StartMultipartUploadRequest request, 
          IAmazonS3 s3Client,   
          CancellationToken cancellationToken)
     {
          try
          {
               var key = $"{request.ContentType}/{Guid.NewGuid()}";

               var startMultipartRequest = new InitiateMultipartUploadRequest()
               {
                    BucketName = "main-bucket",
                    Key = key,
                    ContentType = request.ContentType,
                    Metadata =
                    {
                         ["file-name"] = request.FileName
                    }
               };
               
               var response = await s3Client
                    .InitiateMultipartUploadAsync(startMultipartRequest, cancellationToken);

               return Results.Ok(new
               {
                    key,
                    uploadId = response.UploadId
               });
          }
          catch (AmazonS3Exception e)
          {
               return Results.BadRequest(e.Message); 
          }
     }
}