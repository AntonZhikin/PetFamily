using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Species;

namespace PetFamily.Species.Infrastructure.Configurations.Read;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasColumnName("species_id");
    }
}