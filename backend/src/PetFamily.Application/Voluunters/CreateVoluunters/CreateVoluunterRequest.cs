namespace PetFamily.Application.Voluunters.CreateVoluunters;

public record CreateVoluunterRequest(
    string descriptions, 
    string phoneNumbers, 
    string experienceYears, 
    string name, 
    string surname, 
    string secondname);
