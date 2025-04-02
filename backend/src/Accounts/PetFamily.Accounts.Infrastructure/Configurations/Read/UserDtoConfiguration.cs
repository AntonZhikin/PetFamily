using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.DTOs.Accounts;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read;

public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        builder.ToTable("users");

        builder.HasKey(v => v.Id);
        
        builder.Property(u => u.Email)
            .HasColumnName("email");
        
        builder.HasOne(u => u.ParticipantAccount)
            .WithOne()
            .HasForeignKey<ParticipantAccountDto>(p => p.UserId)
            .IsRequired(false);
        
        builder.HasOne(u => u.AdminAccount)
            .WithOne()
            .HasForeignKey<AdminAccountDto>(p => p.UserId)
            .IsRequired(false);
    }
}