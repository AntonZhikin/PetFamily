using PetFamily.Species.Application.Species.Commands.Create;

namespace PetFamily.Species.Presentation.Species.Request;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() =>
        new CreateSpeciesCommand(Name);
};
