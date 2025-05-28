using PetFamily.Core.Abstractions;

namespace PetFamily.Disscusion.Application.DiscussionManagement.Queries;

public record GetByIdQuery(Guid DiscussionId) : IQuery;