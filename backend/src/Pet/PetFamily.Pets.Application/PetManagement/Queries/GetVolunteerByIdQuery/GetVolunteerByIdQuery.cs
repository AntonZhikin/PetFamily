using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Queries.GetVolunteerByIdQuery;

public record GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;
