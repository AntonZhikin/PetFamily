using PetFamily.Application.Abstractions;
using PetFamily.Application.Species.Dtos;

namespace PetFamily.Application.Species.Create;

public record CreateSpeciesCommand(string Name) : ICommand;
