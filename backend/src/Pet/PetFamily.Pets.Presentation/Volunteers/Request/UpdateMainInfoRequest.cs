using PetFamily.Pets.Application.PetManagement.Commands.UpdateMainInfo;

namespace PetFamily.Pets.Controllers.Volunteers.Request;

public record UpdateMainInfoRequest(
    string? Descriptions,
    string? PhoneNumbers,
    string? ExperienceYears,
    string? Name,
    string? Surname,
    string? SecondName)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) => 
        new UpdateMainInfoCommand(
            volunteerId,
            Descriptions,
            PhoneNumbers,
            ExperienceYears,
            Name,
            Surname,
            SecondName);
}


