using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Kernel;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.Pets.Domain.AggregateRoot;

namespace PetFamily.Pets.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        
        builder.HasKey(x => x.Id);

        builder.HasMany(v => v.Pets)
            .WithOne();
        
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
        
        builder.ComplexProperty(v => v.FullName, g =>
        {
            g.IsRequired();
            g.Property(c => c.Name).HasColumnName("name");
            g.Property(c => c.Surname).HasColumnName("surname");
            g.Property(c => c.SecondName).HasColumnName("secondName");
        });

        builder.ComplexProperty(p => p.PhoneNumber, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("phonenumber");
        });
        
        builder.ComplexProperty(p => p.Description, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("description");
        });
    }
}