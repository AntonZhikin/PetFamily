using PetFamily.Core.Abstractions;

namespace PetFamily.Pets.Application.PetManagement.Commands.DeleteHard;

public record DeleteVolunteerHardCommand (Guid VolunteerId) : ICommand;