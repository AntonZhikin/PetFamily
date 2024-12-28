using PetFamily.Core.Abstractions;
using PetFamily.Core.DTOs.ValueObject;

namespace PetFamily.Pets.Application.PetManagement.Commands.UploadFilesToPet;

public record UploadFileToPetCommand(
    Guid VolunteerId, 
    Guid PetId, 
    IEnumerable<UploadFileDto> Files) : ICommand;