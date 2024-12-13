using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.PetManagement.AggregateRoot;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer, 
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        return volunteer.Id;
    }

    public Guid Save(Volunteer volunteer)
    {
        _dbContext.Volunteers.Attach(volunteer);
        
        return volunteer.Id.Value;
    }
    
    public Guid Delete(Volunteer volunteer)
    {
        _dbContext.Volunteers.Remove(volunteer);
        
        return volunteer.Id.Value;
    }

    public async Task<Guid> DeleteHard(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
    }
    
    public async Task<Result<Volunteer, Error>> GetById(
        VolunteerId volunteerId,
        CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v =>v.Pets)
            .FirstOrDefaultAsync(v =>v.Id == volunteerId, cancellationToken: cancellationToken);

        if (volunteer is null)
        {
            return Errors.General.NotFound(volunteerId);
        }

        return volunteer;
    }
}