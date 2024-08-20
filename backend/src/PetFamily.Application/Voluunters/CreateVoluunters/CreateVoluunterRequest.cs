namespace PetFamily.Application.Voluunters.CreateVoluunters;

public record CreateVoluunterRequest(
    string descriptions, 
    string phoneNumbers, 
    string experienceYears, 
    string countPetInHome, 
    string countPetFoundHomes, 
    string countPetHealing, 
    string name, 
    string surname, 
    string secondname);
