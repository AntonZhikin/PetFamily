using PetFamily.Application.PetManagement.Queries.GetPetById;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record GetPetByIdRequest(Guid PetId)
{
    public GetPetByIdQuery ToQuery() => new GetPetByIdQuery(PetId);
}