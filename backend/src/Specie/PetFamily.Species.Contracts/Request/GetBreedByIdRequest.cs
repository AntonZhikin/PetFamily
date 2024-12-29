namespace PetFamily.Species.Contracts.Request;

public record GetBreedByIdRequest(Guid SpecieId, Guid BreedId);
