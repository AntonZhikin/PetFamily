namespace FilesService.Contract;

public record GetFilesPresignedUrlsRequest(IEnumerable<Guid> FileIds, string? BucketName);