namespace PetFamily.Application.DTOs.ValueObject;

public record UploadFileDto(Stream Content, string FileName);