namespace PetFamily.Domain.Pet.PetLists;

public record RequisiteList
{
    private RequisiteList() { }
    public IReadOnlyList<Requisite> Requisites { get; }

    public RequisiteList(IReadOnlyList<Requisite> requisites)
    {
        Requisites = requisites;
    }
}