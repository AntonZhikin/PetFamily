namespace PetFamily.Application.Files;

public record FileData(Stream Stream, FileInfo Info);

public record FileContent(Stream Stream, string ObjectName);
