using Amazon.S3;
using Amazon.S3.Model;
using FilesService.MongoDataAccess;

namespace FilesService.Jobs;

public class ConfirmConsistencyJob(
    IFileRepository repository,
    IAmazonS3 s3Client,
    ILogger<ConfirmConsistencyJob> _logger)
{
    public async Task<IResult> Execute(Guid fileId, string key, string bucketName, CancellationToken ct)
    {
        var getFileFromDbResult = await repository.Get(fileId, ct);

        var metaDataRequest = new GetObjectMetadataRequest
        {
            BucketName = bucketName,
            Key = key,
        };
        var getFileFromS3Result = await s3Client.GetObjectMetadataAsync(metaDataRequest, cancellationToken: ct);

        var isUploadSuccess = getFileFromDbResult.IsSuccess && getFileFromS3Result != null;

        if (isUploadSuccess) return Results.Ok();

        try
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };
            await s3Client.DeleteObjectAsync(deleteObjectRequest, ct);
            _logger.LogInformation("Файл с id = {fileId} удалён из S3", fileId);

            await repository.Remove(fileId, ct);
            _logger.LogInformation("Файл с id = {fileId} удалён из БД", fileId);
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Results.BadRequest(ex.Message);
        }
    }
}