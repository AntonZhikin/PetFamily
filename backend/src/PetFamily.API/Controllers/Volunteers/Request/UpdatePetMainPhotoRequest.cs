using PetFamily.Application.PetManagement.Commands.UpdatePetMainPhoto;
using PetFamily.Domain.PetManagement.ValueObjects;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record UpdatePetMainPhotoRequest(string PathToStorage);
