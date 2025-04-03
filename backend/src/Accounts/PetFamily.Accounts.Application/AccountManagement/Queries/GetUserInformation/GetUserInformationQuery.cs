using PetFamily.Core.Abstractions;

namespace PetFamily.Accounts.Application.AccountManagement.Queries.GetUserInformation;

public record GetUserInformationQuery(Guid UserId) : IQuery;