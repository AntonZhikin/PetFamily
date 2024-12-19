using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteerByIdQuery;

public record GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;
