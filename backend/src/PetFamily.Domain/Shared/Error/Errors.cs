namespace PetFamily.Domain.Shared.Error;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $"for Id '{id}'";
            return Error.Validation("record.not.found", $"record not found{forId} :");
        }
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("lenght.is.invalid", $"invalid{label}lenght");
        }
        public static Error Found(Guid? id = null)
        {
            var forId = id == null ? "" : $"for Id '{id}'";
            return Error.Validation("record found", $"record found{forId} :");
        }
    }
}