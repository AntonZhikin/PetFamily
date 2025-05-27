using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Core.DTOs.Accounts;
using PetFamily.Core.DTOs.ValueObject;

namespace PetFamily.Accounts.Infrastructure.Configurations.Read;

public class VolunteerAccountDtoConfiguration :
    IEntityTypeConfiguration<VolunteerAccountDto>
{
    public void Configure(EntityTypeBuilder<VolunteerAccountDto> builder)
    {
        builder.ToTable("volunteer_accounts");
        
        builder.HasKey(v => v.VolunteerAccountId);
        
        builder.Property(x => x.VolunteerAccountId)
            .HasColumnName("id");
        
        builder.Property(v => v.UserId)
            .HasColumnName("user_id");
    }
}