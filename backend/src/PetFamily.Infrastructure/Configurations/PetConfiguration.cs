using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Pet;
using PetFamily.Domain.PetManagement.Entity;
using PetFamily.Domain.PetManagement.Ids;
using PetFamily.Domain.Shared;


namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(x => x.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));
        
        builder.ComplexProperty(c => c.Position, b =>
        {
            b.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("Position");
        });
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
        
        builder.ComplexProperty(c => c.Name, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("name")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
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
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.PetHealthInfo, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("petHealthInfo")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Address, b =>
        {
            b.IsRequired();
            b.Property(p => p.City).
                HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            b.Property(p => p.Street)
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.PhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("phone_number")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Weight, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("weight")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.Height, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("height")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });
        
        builder.ComplexProperty(c => c.IsNeutered, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("isNeutered")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        });
        
        builder.Property(p => p.DateOfBirth)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
        
        builder.Property(p => p.IsVaccine)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);

        builder.Property(c => c.HelpStatus)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);

        
        builder.Property(p => p.DateCreate)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);

        builder.OwnsOne(p => p.Photos, pb =>
        {
            pb.ToJson("petPhotos");

            pb.OwnsMany(d => d.PetPhotos, fileBuilder =>
            {
                fileBuilder.Property(f => f.Path)
                    .HasConversion(
                        p => p.Path,
                        value => PhotoPath.Create(value).Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT)
                    .HasColumnName("file_path");
                
                fileBuilder.Property(c => c.IsMain)
                    .IsRequired()
                    .HasColumnName("is_main"); ;
            });
        });

        builder.OwnsOne(p => p.Requisites, rb => 
        {
            rb.ToJson("requisite");

            rb.OwnsMany(r => r.Requisites, rf =>
            {
                rf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT)
                    .HasColumnName("name");
                rf.Property(g => g.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT)
                    .HasColumnName("title");
            });
        });
        
        
    }
}