using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.ComplexProperty(c => c.Name, b =>
        {
            b.IsRequired();
            b.Property(p => p.Name).HasColumnName("Name").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });

        builder.ComplexProperty(c => c.Species, b =>
        {
            b.IsRequired();
            b.Property(p => p.Species).HasColumnName("Species").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });

        builder.ComplexProperty(c => c.Description, b =>
        {
            b.IsRequired();
            b.Property(p => p.Description).HasColumnName("Description").HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });

        builder.ComplexProperty(c => c.Breed, b =>
        {
            b.IsRequired();
            b.Property(p => p.Breed).HasColumnName("Breed").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
            
        });

        builder.ComplexProperty(c => c.Color, b =>
        {
            b.IsRequired();
            b.Property(p => p.Color).HasColumnName("Color").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.PetHealthInfo, b =>
        {
            b.IsRequired();
            b.Property(p => p.PetHealthInfo).HasColumnName("PetHealthInfo").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Address, b =>
        {
            b.IsRequired();
            b.Property(p => p.City).HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
            b.Property(p => p.Street).HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Weight, b =>
        {
            b.IsRequired();
            b.Property(p => p.Weight).HasColumnName("Weight").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Height, b =>
        {
            b.IsRequired();
            b.Property(p => p.Height).HasColumnName("Heigh").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.IsNeutered, b =>
        {
            b.IsRequired();
            b.Property(p => p.IsNautered).HasColumnName("IsNeutered").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });

        
        builder.Property(p => p.DateOfBirth)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        
        builder.Property(p => p.IsVaccine)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        
        builder.ComplexProperty(c => c.Status, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value).HasColumnName("Status").HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });

        
        builder.Property(p => p.DateCreate)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

        

        builder.OwnsOne(p => p.Details, pb =>
        {
            pb.ToJson();

            pb.OwnsMany(d => d.PetPhotos, pf =>
            {
                pf.Property(f => f.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
                pf.Property(f => f.IsMain)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
            });
        });

        builder.OwnsOne(p => p.ReqDetails, rb => 
        {
            rb.ToJson();

            rb.OwnsMany(r => r.Requisites, rf =>
            {
                rf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
                rf.Property(g => g.Title)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            });
        });


        // builder.HasMany(p => p.Requisites)
        //     .WithOne()
        //     .HasForeignKey("pet_id");

        // builder.HasMany(p => p.PetPhotos)
        //     .WithOne()
        //     .HasForeignKey("pet_id");
    }
}