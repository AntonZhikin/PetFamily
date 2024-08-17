namespace PetFamily.Domain.Pet;

public record ReqList()
{
    public List<Requisite> Requisites { get; private set; }
}