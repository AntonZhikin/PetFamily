using PetFamily.Domain.Pet;

namespace PetFamily.Application.FileProvider;

public record FileInfo(
    PhotoPath PhotoPath,
    string BucketName);