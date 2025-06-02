namespace PetFamily.Core.DTOs.Pets;

public record MediaFileDto(
    string BucketName,
    string FileKey,
    string FileName,
    string FileType,
    bool? IsMain);