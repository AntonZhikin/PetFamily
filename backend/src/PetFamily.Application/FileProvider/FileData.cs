using PetFamily.Domain.Pet;

namespace PetFamily.Application.FileProvider;

public record FileData(Stream Stream, PhotoPath PhotoPath, string BucketName);

public record FileContent(Stream Stream, string ObjectName);
