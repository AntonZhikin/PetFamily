namespace PetFamily.Core.DTOs.Accounts;

public class UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public AdminAccountDto? AdminAccount { get; init; }
    public ParticipantAccountDto? ParticipantAccount { get; init; }
    
    public VolunteerAccountDto? VolunteerAccount { get; init; }
}