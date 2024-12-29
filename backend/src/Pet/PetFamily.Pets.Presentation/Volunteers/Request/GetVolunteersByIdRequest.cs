using PetFamily.Pets.Application.PetManagement.Queries.GetVolunteerByIdQuery;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record GetVolunteerByIdRequest(Guid VolunteerId)
{
    public GetVolunteerByIdQuery ToQuery() => new (VolunteerId);
}