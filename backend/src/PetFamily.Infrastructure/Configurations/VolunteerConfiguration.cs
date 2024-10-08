using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

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

        /*builder.ComplexProperty(p => p.CountPetInHomes, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("countPetInHome");
        });
        
        builder.ComplexProperty(p => p.CountPetFoundHomes, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("countPetFoundHome");
        });
        
        builder.ComplexProperty(p => p.CountPetHealing, g =>
        {
            g.IsRequired();
            g.Property(c => c.Value).HasColumnName("countPetHealing");
        });
        
        builder.Property(v => v.CountPetHealing)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);*/
        
        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("vol_id");

        builder.OwnsOne(r => r.ReqListDetails, rb => 
        {
            rb.ToJson();

            rb.OwnsMany(s => s.RequisiteForHelps, rf =>
            {
                rf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
                rf.Property(g => g.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            });
        });
        
        builder.OwnsOne(s => s.SocDetails, sb => 
        {
            sb.ToJson();

            sb.OwnsMany(s => s.SocialMedias, sf =>
            {
                sf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
                sf.Property(g => g.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            });
        });

        
        
        
        // builder.HasMany(v => v.SocialMedias)
        //     .WithOne()
        //     .HasForeignKey("vol_id");
        //
        // builder.HasMany(v => v.RequisiteForHelps)
        //     .WithOne()
        //     .HasForeignKey("vol_id");
    }
}