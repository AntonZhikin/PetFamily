using Amazon.S3;
using Amazon.S3.Model;

namespace FilesService.Features;

public static class UploadPresignedPartUrl
{
    private record UploadPresignedPartUrlRequest(
        string UploadId,
        int PartNumber);

    public sealed class EndPoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/{key}/presigned-part", Handler);
        }
    }

    private static async Task<IResult> Handler(
        string key,
        UploadPresignedPartUrlRequest request,
        IAmazonS3 s3Client,
        CancellationToken cancellationToken)
    {
        try
        {
            var presignedRequest = new GetPreSignedUrlRequest()
            {
                BucketName = "main-bucket",
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(24),
                Protocol = Protocol.HTTP,
                UploadId = request.UploadId,
                PartNumber = request.PartNumber
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