using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    
    
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
         _minioClient = minioClient;
         _logger = logger;
    }
    
    
    public async Task<Result<string, Error>> UploadFile(FileData fileData, string bucketName, CancellationToken cancellationToken = default)
    {
        try
        {
            await EnsureBucketExistsAsync(bucketName, cancellationToken);

            var path = Guid.NewGuid();
        
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(fileData.Stream )
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(path.ToString());
        
            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return result.ObjectName;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }
    
    
    public async Task<Result<bool, Error>> DeleteFile(FileData fileData, string bucketName, CancellationToken cancellationToken = default)
    {
        try
        {
            await EnsureBucketExistsAsync(bucketName, cancellationToken);

            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileData.ObjectName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
            
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }
    
    public async Task<Result<string, Error>> GetFile(FileData fileData, string bucketName, CancellationToken cancellationToken = default)
    {
        try
        {
            PresignedGetObjectArgs getObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileData.ObjectName)
                .WithExpiry(60 * 60 * 24);
            

            var result = await _minioClient.PresignedGetObjectAsync(getObjectArgs);
            
            return result;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Error.Failure("file.get", "Fail to get file in minio");
        }
    }
    
    public async Task EnsureBucketExistsAsync(string bucketName, CancellationToken cancellationToken)
    {
        var bucketExistArgs = new BucketExistsArgs()
            .WithBucket(bucketName);

        var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
        if (!bucketExist)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }
    }
}