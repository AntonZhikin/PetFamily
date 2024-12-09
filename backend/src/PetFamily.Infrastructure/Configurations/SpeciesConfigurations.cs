using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.AggregateRoot;
using PetFamily.Domain.Speciess;
using PetFamily.Domain.Speciess.SpeciesID;

namespace PetFamily.Infrastructure.Configurations;

public class SpeciesConfigurations : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));
        
        builder.ComplexProperty(x => x.Name, tb =>
        {
            tb.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT)
                .HasColumnName("name");
        });

        builder.HasMany(x => x.Breeds)
            .WithOne();
        
        // builder.OwnsOne(p => p.Breeds, pb =>
        // {
        //     pb.ToJson();
        //
        //     pb.OwnsMany(d =>d.Breeds, pf=>
        //     {
        //         pf.Property(f => f.Breeds)
        //             .IsRequired()
        //             .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        //     });
        // });

    }
}