using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;

namespace PetFamily.Application.PetManagement.Commands.UploadFilesToPet;

public record UploadFileToPetCommand(
    Guid VolunteerId, 
    Guid PetId, 
    IEnumerable<UploadFileDto> Files) : ICommand;