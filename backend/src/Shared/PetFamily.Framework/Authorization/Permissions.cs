namespace PetFamily.Framework.Authorization;

public static class Permissions
{
    public static class Volunteer
    {
        public const string CreateVolunteer = "volunteer.create";
        public const string ReadVolunteer = "volunteer.read";
        public const string DeleteVolunteer = "volunteer.delete";
        public const string UpdateVolunteer = "volunteer.update";
    }
}