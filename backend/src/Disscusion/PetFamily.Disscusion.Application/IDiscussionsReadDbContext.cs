using PetFamily.Core.DTOs.Discussion;

namespace PetFamily.Disscusion.Application;

public interface IDiscussionsReadDbContext
{
    IQueryable<DiscussionDto> Discussions { get; }
    IQueryable<MessageDto> Messages { get; }
}