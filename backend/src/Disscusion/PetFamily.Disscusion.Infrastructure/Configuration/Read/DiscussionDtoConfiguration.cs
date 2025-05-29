using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.DTOs.Discussion;

namespace PetFamily.Disscusion.Infrastructure.Configuration.Read;

public class DiscussionDtoConfiguration :
    IEntityTypeConfiguration<DiscussionDto>
{
    public void Configure(EntityTypeBuilder<DiscussionDto> builder)
    {
        builder.ToTable("discussion");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Id)
            .HasColumnName("id");
        
        builder.Property(s => s.RequestId)
            .IsRequired()
            .HasColumnName("request_id");
        
        builder.HasMany(d => d.Messages)
            .WithOne()
            .HasForeignKey(m => m.DiscussionId);
    }
}