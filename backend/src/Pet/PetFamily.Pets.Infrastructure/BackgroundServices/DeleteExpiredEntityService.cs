using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core;
using PetFamily.Pets.Domain.AggregateRoot;
using PetFamily.Pets.Infrastructure.DbContext;

namespace PetFamily.Pets.Infrastructure.BackgroundServices;

public class DeleteExpiredEntityService
{
    private readonly WriteDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpiredEntityService(WriteDbContext context,
        [FromKeyedServices(Modules.Pets)] IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Process(int daysBeforeDelete, CancellationToken cancellationToken)
    {
        var volunteerWithPets = await GetVolunteerWithPetsAsync(cancellationToken);

        foreach (var volunteer in volunteerWithPets)
        {
            volunteer.DeleteExpiredPets(daysBeforeDelete);
        }

        await _unitOfWork.SaveChanges(cancellationToken);
    }

    private async Task<IEnumerable<Volunteer>> GetVolunteerWithPetsAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Volunteers
            .Include(v => v.Pets)
            .ToListAsync(cancellationToken);
    }
}