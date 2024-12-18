using PetFamily.Application.Species.Create;

namespace PetFamily.API.Controllers.Species.Request;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() =>
        new CreateSpeciesCommand(Name);
};
