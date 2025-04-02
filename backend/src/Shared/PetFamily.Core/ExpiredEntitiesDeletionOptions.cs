namespace PetFamily.Core;

public class ExpiredEntitiesDeletionOptions
{
    public const string ExpiredEntitiesDeleteRemoveService = "ExpiredEntitiesDeleteRemoveService";
    
    public int DaysBeforeDelete { get; init; } = 0;

    public int WorkingCycleInHours { get; init; } = 0;
}