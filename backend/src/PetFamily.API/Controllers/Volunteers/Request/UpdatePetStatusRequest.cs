using PetFamily.Application.PetManagement.Commands.UpdatePetStatus;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record UpdatePetStatusRequest(string NewStatus);