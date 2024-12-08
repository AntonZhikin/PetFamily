using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    private const int MAX_DEGREE_OF_PARALLELISM = 10;

    
    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
         _minioClient = minioClient;
         _logger = logger;
    }
    
    
    public async Task<Result<IReadOnlyList<PhotoPath>, Error>> UploadFiles(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = filesData.ToList();

        try
        {
            await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file =>
                await PutObject(file, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);

            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload files in minio, files amount: {amount}", filesList.Count);

            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }
    
    
    public async Task<Result<bool, Error>> DeleteFile
        (FileData filesData, CancellationToken cancellationToken = default)
    {
        try
        {
            await CreateBucketIfNotExists(filesData.BucketName, cancellationToken);

            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(filesData.BucketName)
                .WithObject(filesData.PhotoPath.Path);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
            
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Error.Failure("file.delete", "Fail to delete file in minio");
        }
    }
    
    public async Task<Result<string, Error>> GetFile(FileContent fileContent, string bucketName, CancellationToken cancellationToken = default)
    {
        try
        {
            PresignedGetObjectArgs getObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileContent.ObjectName)
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
    
    private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<FileData> filesData,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = [..filesData.Select(file => file.BucketName)];

        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            
            var bucketExist = await _minioClient
                .BucketExistsAsync(bucketExistArgs, cancellationToken);
            
            if (bucketExist == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);
                
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }
    
    private async Task<Result<PhotoPath, Error>> PutObject(
        FileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileData.BucketName)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(fileData.PhotoPath.Path);

        try
        {
            await _minioClient
                .PutObjectAsync(putObjectArgs, cancellationToken);

            return fileData.PhotoPath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Fail to upload file in minio with path {path} in bucket {bucket}",
                fileData.PhotoPath.Path,
                fileData.BucketName);

            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
    
    private async Task CreateBucketsIfNotExist(
        IEnumerable<string> buckets,
        CancellationToken cancellationToken = default)
    {
        HashSet<String> bucketNames = [..buckets];

        foreach (var bucketName in bucketNames)
        {
            await CreateBucket(bucketName, cancellationToken);
        }
    }
    
    private async Task CreateBucketIfNotExists(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var bucketExist = await IsBucketExist(bucketName, cancellationToken);
        
        if (bucketExist == false)
        {
            await CreateBucket(bucketName, cancellationToken);
        }
    }
    
    private async Task<bool> IsBucketExist(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var bucketExistArgs = new BucketExistsArgs().WithBucket(bucketName);
        var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

        return bucketExist;
    }
    
    private async Task CreateBucket(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
        await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
    }
    
}