namespace PetFamily.Framework.Authorization;

public static class PermissionsConfig
{
    public static class Accounts
    {
        public const string Read = "accounts.read";
        public const string Update = "accounts.update";
    }
    
    public static class VolunteerRequests
    {
        public const string Create = "request.create";
        public const string Review = "request.review";
        public const string Read = "request.read";
        public const string Update = "request.update";
        public const string Delete = "request.delete";
    }
    
    public static class Discussions
    {
        public const string Create = "discussions.create";
        public const string Read = "discussions.read";
        public const string Update = "discussions.update";
        public const string Delete = "discussions.delete";
    }
    
    public static class Volunteers
    {
        public const string Create = "volunteers.create";
        public const string Read = "volunteers.read";
        public const string Update = "volunteers.update";
        public const string Delete = "volunteers.delete";
    }
    
    public static class Pets
    {
        public const string Read = "pets.read";
    }
    
    public static class Species
    {
        public const string Create = "species.create";
        public const string Read = "species.read";
        public const string Update = "species.update";
        public const string Delete = "species.delete";
    }
    
    public static class Breeds
    {
        public const string Create = "breeds.create";
        public const string Read = "breeds.read";
        public const string Update = "breeds.update";
        public const string Delete = "breeds.delete";
    }
    
    public static class Files
    {
        public const string Upload = "files.upload";
        public const string Read = "files.read";
        public const string Delete = "files.delete";
    }
}