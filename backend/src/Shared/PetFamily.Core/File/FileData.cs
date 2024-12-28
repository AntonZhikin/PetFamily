namespace PetFamily.Core.File;

public record FileData(Stream Stream, FileInfo Info);

public record FileContent(Stream Stream, string ObjectName);
