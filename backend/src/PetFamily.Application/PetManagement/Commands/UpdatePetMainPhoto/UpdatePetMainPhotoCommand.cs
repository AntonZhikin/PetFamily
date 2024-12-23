using PetFamily.Application.Abstractions;
using PetFamily.Domain.PetManagement.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.UpdatePetMainPhoto;

public record UpdatePetMainPhotoCommand(PhotoPath PhotoPath, Guid VolunteerId, Guid PetId) : ICommand;
