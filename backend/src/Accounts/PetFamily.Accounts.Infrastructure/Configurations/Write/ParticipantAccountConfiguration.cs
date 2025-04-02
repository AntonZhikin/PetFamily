using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.Accounts;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write;

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