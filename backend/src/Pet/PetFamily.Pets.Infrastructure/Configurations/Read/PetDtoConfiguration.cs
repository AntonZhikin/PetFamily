using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Kernel;

namespace PetFamily.Pets.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(i => i.Photos)
            .HasConversion(
                photos => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer
                    .Deserialize<PetPhotoDto[]>(json, JsonSerializerOptions.Default)!);

        builder.ComplexProperty(c => c.Address, b =>
        {
            b.IsRequired();
            b.Property(p => p.City).HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            b.Property(p => p.Street)
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });

        builder.ComplexProperty(p => p.SpeciesBreedDto, psd =>
        {
            psd.Property(p => p.SpeciesId)
                .IsRequired()
                .HasColumnName("species_id");

            psd.Property(p => p.BreedId)
                .IsRequired()
                .HasColumnName("breed_id");
        });
        
        
        // builder.Property(c => c.Position)
        //     .IsRequired()
        //     .HasColumnName("position");
    }
}