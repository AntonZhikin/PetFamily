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
        builder.HasKey(x => x.Id);
        
        // builder.ToTable("participant_accounts");
        // builder.HasOne(a => a.User)
        //     .WithOne()
        //     .HasForeignKey<ParticipantAccount>(a => a.UserId);
    }
}