namespace PetFamily.Core.DTOs.ValueObject;

public record UploadFileDto(Stream Content, string FileName);