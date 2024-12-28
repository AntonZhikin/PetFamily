 namespace PetFamily.Kernel;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}