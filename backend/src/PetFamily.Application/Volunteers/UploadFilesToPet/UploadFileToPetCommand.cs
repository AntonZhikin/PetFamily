using PetFamily.Application.Volunteers.DTOs;

namespace PetFamily.Application.Volunteers.UploadFilesToPet;

public record UploadFileToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files);