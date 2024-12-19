using PetFamily.Application.DTOs;
using PetFamily.Application.DTOs.ValueObject;
using PetFamily.Application.PetManagement.Commands.Create;

namespace PetFamily.API.Controllers.Volunteers.Request;

public record CreateVolunteerRequest(
    string Descriptions,
    string PhoneNumbers,
    string ExperienceYears,
    string Name,
    string Surname,
    string SecondName,
    SocialNetworkListDto SocialNetworkList,
    AssistanceDetailListDto AssistanceDetailList
)
{
    public CreateVolunteerCommand ToCommand() => 
        new CreateVolunteerCommand(
            Descriptions,
            PhoneNumbers,
            ExperienceYears,
            Name,
            Surname,
            SecondName,
            SocialNetworkList,
            AssistanceDetailList);
}
