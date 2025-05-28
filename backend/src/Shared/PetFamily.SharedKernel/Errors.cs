namespace PetFamily.Kernel;

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
        public static Error Failure(Guid? id = null)
        {
            return Error.Failure("failure", "failure");
        }
        
        
    }

    public static class User
    {
        public static Error InvalidCredentials(string? name = null)
        {
            return Error.Validation("credentials.is.invalid", $"credentials is invalid");
        }
    }
    
    public static class Review
    {
        public static Error Failure(string? name = null)
        {
            return Error.Failure("failure", "failure");
        }
    }
    
    public static class Tokens
    {
        public static Error ExpiredToken()
        {
            return Error.Validation("token.is.expired", "your token is expired");
        }
        public static Error InvalidToken()
        {
            return Error.Validation("token.is.invalid", "your token is invalid");
        }
    }

    public static class Discussion
    {
        public static Error ClosedDiscussion()
        {
            return Error.Failure("disscusion", "this disscusion is closed");
        }

        public static Error UserNotInDiscussion()
        {
            return Error.Failure("disscusion", "user not found of this disscusion");
        }
        
        public static Error AlreadyClosed()
        {
            return Error.Failure("disscusion", "already clodes");
        }
        public static Error NotCreated()
        {
            return Error.Failure("disscusion", "disscusion not created");
        }
    }
}