namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand (
    Guid VolunteerId,
    string? Descriptions, 
    string? PhoneNumbers, 
    string? ExperienceYears, 
    string? Name, 
    string? Surname, 
    string? SecondName  );
    
// public record UpdateMainInfoDto(
//     string? Descriptions, 
//     string? PhoneNumbers, 
//     string? ExperienceYears, 
//     string? Name, 
//     string? Surname, 
//     string? SecondName );