using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetPetById;

public record GetPetByIdQuery(Guid PetId) : IQuery;
