using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Disscusion.Domain.Entity;
using PetFamily.Disscusion.Domain.ValueObject;

namespace PetFamily.Disscusion.Infrastructure.Configuration.Write;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("message");
        
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Id)
            .IsRequired()
            .HasColumnName("id");
        
        builder.Property(v => v.Content)
            .HasConversion(
                id => id.Value,
                value => Content.Create(value).Value)
            .IsRequired()
            .HasColumnName("message_content");
        
        builder.Property(s => s.SenderId)
            .IsRequired()
            .HasColumnName("sender_id");
        
        builder.Property(s => s.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        
        builder.Property(s => s.IsEdited)
            .IsRequired()
            .HasColumnName("is_edited");
    }
}