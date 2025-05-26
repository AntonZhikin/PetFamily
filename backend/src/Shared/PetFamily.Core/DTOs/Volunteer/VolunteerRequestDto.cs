namespace PetFamily.Core.DTOs.Volunteer;

public class VolunteerRequestDto
{
    public Guid Id { get; init; }
    public Guid? AdminId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string? SecondName { get; init; } = string.Empty;
    public int Age { get; init; }
    public int Grade { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public string? RejectionComment { get; init; }
}