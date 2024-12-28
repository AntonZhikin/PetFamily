/*using AutoFixture;
using PetFamily.Pets.Application.PetManagement.Commands.AddPet;

namespace TestProject1;

public static class FixtureExtensions
{
    public static AddPetCommand CreateAddPetCommand(
        this Fixture fixture,
        Guid volunteerId)
    {
        return fixture.Build<AddPetCommand>()
            .With(c => c.VolunteerId, volunteerId)
            .Create();
    }
}*/