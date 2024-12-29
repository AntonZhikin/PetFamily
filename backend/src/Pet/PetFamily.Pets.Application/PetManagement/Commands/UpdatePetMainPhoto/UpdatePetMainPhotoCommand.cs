using PetFamily.Core;
using PetFamily.Core.Abstractions;
using PetFamily.Kernel.ValueObject;
using PetFamily.Pets.Domain.ValueObjects;

namespace PetFamily.Pets.Application.PetManagement.Commands.UpdatePetMainPhoto;

public record UpdatePetMainPhotoCommand(PhotoPath PhotoPath, Guid VolunteerId, Guid PetId) : ICommand;
