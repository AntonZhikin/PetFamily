using Amazon.S3;
using Amazon.S3.Model;

namespace FilesService.Features;

public static class UploadPresignedUrl
{
    private record UploadPresignedUrlRequest(
        string FileName,
        string ContentType,
        long Size);

    public sealed class EndPoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/presigned", Handler);
        }
    }

    private static async Task<IResult> Handler(
        UploadPresignedUrlRequest request,
        IAmazonS3 s3Client,
        CancellationToken cancellationToken)
    {
        var key = Guid.NewGuid();
        try
        {
            var presignedRequest = new GetPreSignedUrlRequest()
            {
                BucketName = "main-bucket",
                Key = $"videos/{key}",
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(24),
                ContentType = request.ContentType,
                Protocol = Protocol.HTTP,
                Metadata =
                {
                    ["file-name"] = request.FileName
                }
            };

            var presignedUrl = await s3Client.GetPreSignedURLAsync(presignedRequest);

            return Results.Ok(new
            {
                key,
                url = presignedUrl
            });
        }
        catch (AmazonS3Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}