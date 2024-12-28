using PetFamily.Kernel.ValueObject;

namespace PetFamily.Core.File;

public record FileInfo(
    PhotoPath PhotoPath,
    string BucketName);