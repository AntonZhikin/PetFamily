using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Pets.Application.PetManagement;
using PetFamily.Pets.Domain.AggregateRoot;
using PetFamily.Pets.Infrastructure.DbContext;

namespace PetFamily.Pets.Infrastructure.Repositories;

public class VolunteersRepository : IVolunteerRepository
{
    private readonly WriteDbContext _dbContext;

    public VolunteersRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer volunteer)
    {
        await _dbContext.Volunteers.AddAsync(volunteer);

        return volunteer.Id;
    }

    public async Task<Guid> DeleteHard(Volunteer volunteer)
    {
        _dbContext.Volunteers.Remove(volunteer);
        
        return volunteer.Id;
    }
    
    public async Task<Guid> DeleteHardPet(Volunteer volunteer)
    {
        _dbContext.Volunteers.Remove(volunteer);
        
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