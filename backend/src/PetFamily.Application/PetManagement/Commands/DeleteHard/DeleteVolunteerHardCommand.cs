using PetFamily.Application.Abstractions;

namespace PetFamily.Application.PetManagement.Commands.DeleteHard;

public record DeleteVolunteerHardCommand (Guid VolunteerId) : ICommand;