using PetFamily.Application.PetManagement.Queries.GetVolunteerByIdQuery;
using PetFamily.Domain.PetManagement.Ids;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record GetVolunteerByIdRequest(Guid VolunteerId)
{
    public GetVolunteerByIdQuery ToQuery() => new (VolunteerId);
}