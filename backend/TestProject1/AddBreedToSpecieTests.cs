using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Species.Commands.AddBreedToSpecies;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.AggregateRoot;
using PetFamily.Domain.SpeciesManagement.Ids;
using PetFamily.Infrastructure.DbContext;

namespace TestProject1;

public class AddBreedToSpecieTests : IClassFixture<IntegrationTestsWebFactory>, IAsyncLifetime
{
    private readonly IntegrationTestsWebFactory _factory;
    private readonly WriteDbContext _writeDbContext;
    private readonly IServiceScope _scope;
    private readonly ICommandHandler<Guid, AddBreedToSpeciesCommand> _sut;
    
    public AddBreedToSpecieTests(IntegrationTestsWebFactory factory)
    {
        _factory = factory;
        
        _scope = _factory.Services.CreateScope();
        _writeDbContext = _scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, AddBreedToSpeciesCommand>>();
    }
    
    [Fact]
    public async Task Add_Breed_To_Specie_Result_Should_Be_Success()
    {
        // Arrange
        var specieId = await SeedSpecie();
        var command = new AddBreedToSpeciesCommand(specieId, "Breed");

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var specieResult = await _writeDbContext.Species.Include(species => species.Breeds)
            .FirstOrDefaultAsync();

        var breedsCount = specieResult.Breeds.Count; 

        breedsCount.Should().Be(1);
    }
    
    private async Task<Guid> SeedSpecie()
    {
        var specie = Species.Create(SpeciesId.NewSpeciesId(), Name.Create("Breed").Value);

        await _writeDbContext.Species.AddAsync(specie.Value);
        await _writeDbContext.SaveChangesAsync();
        return specie.Value.Id;
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        _scope.Dispose();
        
        await _factory.ResetDatabaseAsync();
    }
}