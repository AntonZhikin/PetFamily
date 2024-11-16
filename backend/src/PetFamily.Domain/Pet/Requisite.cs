using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Pet;

public sealed class Requisite
{
    public string Name { get; private set; } = null!;

    public string Title { get; private set; } = null!;
}