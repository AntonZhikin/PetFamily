namespace FilesService.Core.Models;

public static class Errors
{
    public static class Files
    {
        public static Error FailRemove() => Error.Failure("files.remove", "fail to files removed");
    }
}