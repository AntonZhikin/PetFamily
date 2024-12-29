using PetFamily.Pets.Application.PetManagement.Queries.GetPetById;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record GetPetByIdRequest(Guid PetId)
{
    public GetPetByIdQuery ToQuery() => new GetPetByIdQuery(PetId);
}