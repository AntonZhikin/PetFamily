using PetFamily.Application.Abstractions;
using PetFamily.Application.DTOs;
using PetFamily.Application.DTOs.ValueObject;

namespace PetFamily.Application.PetManagement.Commands.UploadFilesToPet;

public record UploadFileToPetCommand(
    Guid VolunteerId, 
    Guid PetId, 
    IEnumerable<UploadFileDto> Files) : ICommand;