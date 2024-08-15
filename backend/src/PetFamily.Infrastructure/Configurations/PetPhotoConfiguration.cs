using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public class PetPhotoConfiguration : IEntityTypeConfiguration<PetPhoto>
{
    public void Configure(EntityTypeBuilder<PetPhoto> builder)
    {
        builder.ToTable("petphotos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Path)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);

        builder.Property(p => p.IsMain)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

    }
}