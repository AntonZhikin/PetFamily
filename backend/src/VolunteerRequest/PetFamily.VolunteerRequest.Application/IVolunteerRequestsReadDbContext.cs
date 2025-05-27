using PetFamily.Core.DTOs.Volunteer;

namespace PetFamily.VolunteerRequest.Application;

public interface IVolunteerRequestsReadDbContext
{
    IQueryable<VolunteerRequestDto> VolunteerRequests { get; }
}