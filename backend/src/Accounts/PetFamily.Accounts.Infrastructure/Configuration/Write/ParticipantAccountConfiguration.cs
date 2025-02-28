using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Core.RolesPermissions;

namespace PetFamily.Accounts.Infrastructure.Configuration.Write;

public class ParticipantAccountConfiguration : IEntityTypeConfiguration<ParticipantAccount>
{
    public void Configure(EntityTypeBuilder<ParticipantAccount> builder)
    {
        builder.ToTable("participant_accounts");

        builder.HasKey(i => i.UserId);
        
        builder.Property(p => p.UserId)
            .HasConversion(
                userId => userId.Value, // Преобразуем в Guid при сохранении
                value => UserId.Create(value).Value // Восстанавливаем UserId из Guid
            )
            .HasColumnName("user_id");
        
        builder.HasOne(p => p.User)
            .WithOne(u => u.Participant)
            .HasForeignKey<ParticipantAccount>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}