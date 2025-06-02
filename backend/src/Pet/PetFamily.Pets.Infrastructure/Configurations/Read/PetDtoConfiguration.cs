using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core;
using PetFamily.Core.DTOs;
using PetFamily.Core.DTOs.Pets;
using PetFamily.Core.DTOs.ValueObject;
using PetFamily.Kernel;

namespace PetFamily.Pets.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

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
        
        builder.Ignore(p => p.AvatarUrl);
            
        builder.OwnsOne(p => p.Avatar, ab =>
        {
            ab.ToJson("avatar");
                
            ab.Property(a => a.FileKey)
                .IsRequired()
                .HasColumnName("key");

            ab.Property(a => a.FileType)
                .HasConversion<string>()
                .IsRequired()
                .HasColumnName("type");
                
            ab.Property(a => a.BucketName)
                .IsRequired()
                .HasColumnName("bucket_name");

            ab.Property(a => a.FileName)
                .IsRequired()
                .HasColumnName("file_name");

            ab.Property(p => p.IsMain)
                .IsRequired(false)
                .HasColumnName("is_main");
        });
            
        builder.Ignore(p => p.PhotosUrls);

        builder.Property(p => p.Photos)
            .HasConversion(
                photos => JsonSerializer
                    .Serialize(photos, JsonSerializerOptions.Default),

                json => JsonSerializer
                    .Deserialize<IReadOnlyList<MediaFileDto>>(
                        json, JsonSerializerOptions.Default)!);
        
    }
}