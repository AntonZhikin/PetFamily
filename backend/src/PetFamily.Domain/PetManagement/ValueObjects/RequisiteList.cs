namespace PetFamily.Domain.Pet.PetLists;

public record RequisiteList
{
    private RequisiteList() { }
    public List<Requisite> Requisites { get; }

    public RequisiteList(IEnumerable<Requisite> requisites)
    {
        Requisites = requisites.ToList();
    }
}