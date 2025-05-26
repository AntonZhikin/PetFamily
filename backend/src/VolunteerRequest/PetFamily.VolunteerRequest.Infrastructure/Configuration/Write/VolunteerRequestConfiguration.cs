using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Kernel.ValueObject.Ids;
using PetFamily.VolunteerRequest.Domain.ValueObject;

namespace PetFamily.VolunteerRequest.Infrastructure.Configuration.Write;

public class VolunteerRequestConfiguration : IEntityTypeConfiguration<Domain.VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<Domain.VolunteerRequest> builder)
    {
        builder.ToTable("volunteer_requests");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerRequestId.Create(value))
            .HasColumnName("id");
        
        builder.ComplexProperty(v => v.FullName, g =>
        {
            g.IsRequired();
            g.Property(c => c.Name).HasColumnName("name");
            g.Property(c => c.Surname).HasColumnName("surname");
            g.Property(c => c.SecondName).HasColumnName("secondName");
        });
        
        builder.ComplexProperty(v => v.VolunteerInfo, g =>
        {
            g.IsRequired();
            g.Property(c => c.Age).HasColumnName("volunteer_info");
        });
        
        builder.Property(s => s.AdminId)
            .IsRequired(false)
            .HasColumnName("admin_Id");
        
        builder.Property(s => s.UserId)
            .HasColumnName("user_Id");
        
        builder.Property(v => v.Status)
            .HasConversion<string>()
            .HasColumnName("status")
            .IsRequired();
        
        builder.Property(v => v.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        
        builder.Property(v => v.RejectionComment)
            .HasConversion(
                i => i.Value,
                value => RejectionComment.Create(value).Value)
            .IsRequired(false)
            .HasColumnName("rejection_comment");
    }
}