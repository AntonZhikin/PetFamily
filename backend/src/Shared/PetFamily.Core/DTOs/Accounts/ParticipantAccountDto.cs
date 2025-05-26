namespace PetFamily.Core.DTOs.Accounts;

public class ParticipantAccountDto
{
    public Guid PartisipantAccountId { get; init; }
    public Guid UserId { get; init; }
    public DateTime? BannedForRequestsUntil { get; set; }
}