using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Species.Domain.SpeciesManagement.Entity;

namespace PetFamily.Species.Infrastructure.Configurations.Write;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value))
            .HasColumnName("breed_id");
                    
        
        builder.ComplexProperty(x => x.Name, tb =>
        {
            tb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT)
                .HasColumnName("name");
        });
    }
}