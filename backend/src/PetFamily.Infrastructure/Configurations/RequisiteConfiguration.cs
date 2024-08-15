using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;


namespace PetFamily.Infrastructure.Configurations;

public class RequisiteConfiguration : IEntityTypeConfiguration<Requisite>
{
    public void Configure(EntityTypeBuilder<Requisite> builder)
    {
        builder.ToTable("requisites");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
    }
}
