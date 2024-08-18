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

        builder.HasKey(v => v.Id);

        builder.ComplexProperty(v => v.FullName, g =>
        {
            g.IsRequired();
            g.Property(c => c.Name).HasColumnName("name");
            g.Property(c => c.Surname).HasColumnName("surname");
            g.Property(c => c.SecondName).HasColumnName("secondName");
        });
        
        builder.Property(v => v.Descriptions)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);

        builder.Property(v => v.ExperienceYears)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        
        builder.Property(v => v.CountPetInHome)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        
        builder.Property(v => v.CountPetFoundHome)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        
        builder.Property(v => v.CountPetHealing)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);
        
        builder.Property(v => v.PhoneNumber)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGHT);

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