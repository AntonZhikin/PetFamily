using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Speciess;

namespace PetFamily.Infrastructure.Configurations;

public class SpeciesConfigurations : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.OwnsOne(p => p.Breeds, pb =>
        {
            pb.ToJson();

            pb.OwnsMany(d =>d.Breeds, pf=>
            {
                pf.HasKey(pf => pf.Id);
                
                pf.Property(f => f.Breeds)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
            });
        });
    }
}