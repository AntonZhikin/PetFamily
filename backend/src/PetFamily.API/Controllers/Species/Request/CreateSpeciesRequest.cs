using PetFamily.Application.Species.Create;
using PetFamily.Application.Species.Dtos;

namespace PetFamily.API.Controllers.Species.Request;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() =>
        new CreateSpeciesCommand(Name);
};
