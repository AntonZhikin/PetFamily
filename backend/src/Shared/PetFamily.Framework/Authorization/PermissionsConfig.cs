namespace PetFamily.Framework.Authorization;

public static class PermissionsConfig
{
    public static class Volunteer
    {
        public const string CreateVolunteer = "volunteer.create";
        public const string ReadVolunteer = "volunteer.read";
        public const string DeleteVolunteer = "volunteer.delete";
        public const string UpdateVolunteer = "volunteer.update";
    }
    
    public static class VolunteerRequests
    {
        public const string Create = "request.create";
        public const string Review = "request.review";
        public const string Read = "request.read";
        public const string Update = "request.update";
        public const string Delete = "request.delete";
    }
}