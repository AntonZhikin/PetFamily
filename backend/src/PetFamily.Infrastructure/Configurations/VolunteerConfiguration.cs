using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.VolunteerID;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("voluunter");

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.ComplexProperty(v => v.FullNames, g =>
        {
            g.IsRequired();
            g.Property(c => c.Name).HasColumnName("name");
            g.Property(c => c.Surname).HasColumnName("surname");
            g.Property(c => c.SecondName).HasColumnName("secondName");
        });

        builder.ComplexProperty(p => p.PhoneNumbers, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("phonenumber");
        });
        
        builder.ComplexProperty(p => p.Descriptions, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("description");
        });
        
        builder.ComplexProperty(p => p.ExperienceYears, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("experienceyears");
        });
        
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("vol_id");

        builder.OwnsOne(r => r.AssistanceDetails, rb => 
        {
            rb.ToJson();

            rb.OwnsMany(s => s.AssistanceDetails, rf =>
            {
                rf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
                rf.Property(g => g.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            });
        });
        
        builder.OwnsOne(s => s.SocialNetworks, sb => 
        {
            sb.ToJson();

            sb.OwnsMany(s => s.SocialNetworks, sf =>
            {
                sf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
                sf.Property(g => g.Link)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            });
        });
    }
}