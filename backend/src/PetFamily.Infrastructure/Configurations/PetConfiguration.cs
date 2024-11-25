using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Pet.PetID;
using PetFamily.Domain.Shared;


namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));
        
        builder.ComplexProperty(c => c.SerialNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("serial_number");
        });
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
        
        builder.ComplexProperty(c => c.Name, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("name")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });

        builder.ComplexProperty(c => c.Description, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("description")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });

        builder.ComplexProperty(c => c.Color, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("color")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.PetHealthInfo, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("petHealthInfo")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Address, b =>
        {
            b.IsRequired();
            b.Property(p => p.City).
                HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
            b.Property(p => p.Street)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.PhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("phone_number")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Weight, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("weight")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Height, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("height")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.IsNeutered, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("isNeutered")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        });
        
        builder.Property(p => p.DateOfBirth)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        
        builder.Property(p => p.IsVaccine)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

        builder.Property(c => c.HelpStatus)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

        
        builder.Property(p => p.DateCreate)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

        builder.OwnsOne(p => p.Photos, pb =>
        {
            pb.ToJson("petphotos");

            pb.OwnsMany(d => d.PetPhotos, pf =>
            {
                pf.Property(f => f.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT)
                    .HasColumnName("path");
                pf.Property(f => f.IsMain)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT)
                    .HasColumnName("IsMain");
            });
        });

        builder.OwnsOne(p => p.Requisites, rb => 
        {
            rb.ToJson("requisite");

            rb.OwnsMany(r => r.Requisites, rf =>
            {
                rf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT)
                    .HasColumnName("Name");
                rf.Property(g => g.Title)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT)
                    .HasColumnName("Title");
            });
        });
        
        
    }
}