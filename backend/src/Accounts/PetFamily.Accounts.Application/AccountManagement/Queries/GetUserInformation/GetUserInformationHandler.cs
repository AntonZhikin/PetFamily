using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.Accounts;

namespace PetFamily.Accounts.Application.AccountManagement.Queries.GetUserInformation;

public class GetUserInformationHandler : IQueryHandler<UserDto, GetUserInformationQuery>
{
    private readonly IAccountsReadDbContext _readDbContext;
    
    public GetUserInformationHandler(IAccountsReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<UserDto> Handle(GetUserInformationQuery query, CancellationToken cancellationToken = default)
    {
        var userDto = await _readDbContext.Users
            .Include(u => u.AdminAccount)
            .Include(u => u.ParticipantAccount)
            .SingleOrDefaultAsync(u => u.Id == query.UserId, cancellationToken);

        if (userDto is null)
        {
            throw new ApplicationException("User not found");
        }
        
        return userDto;
    }
}