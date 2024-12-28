using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;

namespace PetFamily.Species.Infrastructure.Configurations.Write;

public class SpeciesConfigurations : IEntityTypeConfiguration<Domain.SpeciesManagement.AggregateRoot.Species>
{
    public void Configure(EntityTypeBuilder<Domain.SpeciesManagement.AggregateRoot.Species> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value))
            .HasColumnName("species_id");
        
        builder.ComplexProperty(x => x.Name, tb =>
        {
            tb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT)
                .HasColumnName("name");
        });

        builder.HasMany(x => x.Breeds)
            .WithOne()
            .IsRequired();
    }
}