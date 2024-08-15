using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Infrastructure.Configurations;

public class RequisiteForHelpConfiguration : IEntityTypeConfiguration<RequisiteForHelp>
{
    public void Configure(EntityTypeBuilder<RequisiteForHelp> builder)
    {
        builder.ToTable("requisiteforhelp");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
    }
}