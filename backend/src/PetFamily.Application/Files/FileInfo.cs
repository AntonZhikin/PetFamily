using PetFamily.Domain.PetManagement.ValueObjects;

namespace PetFamily.Application.Files;

public record FileInfo(
    PhotoPath PhotoPath,
    string BucketName);