using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.DTOs.Volunteer;

namespace PetFamily.VolunteerRequest.Infrastructure.Configuration.Read;

public class VolunteerRequestDtoConfiguration :
    IEntityTypeConfiguration<VolunteerRequestDto>
{
    public void Configure(EntityTypeBuilder<VolunteerRequestDto> builder)
    {
        builder.ToTable("volunteer_requests");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(s => s.Id)
            .IsRequired()
            .HasColumnName("id");
        
        builder.Property(s => s.AdminId)
            .IsRequired(false)
            .HasColumnName("admin_Id");
        
        builder.Property(s => s.UserId)
            .HasColumnName("user_Id");
        
        builder.Property(v=> v.Name)
            .HasConversion<string>()
            .HasColumnName("name")
            .IsRequired();
        
        builder.Property(v=> v.Surname)
            .HasConversion<string>()
            .HasColumnName("surname")
            .IsRequired();

        builder.Property(v => v.SecondName)
            .HasConversion<string>()
            .HasColumnName("second_name");
        
        builder.Property(v=> v.Status)
            .HasConversion<string>()
            .HasColumnName("status")
            .IsRequired();
        
        builder.Property(v=> v.RejectionComment)
            .HasConversion<string>()
            .HasColumnName("rejection_comment");
    }
}