using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.Accounts;

namespace PetFamily.Accounts.Infrastructure.Configuration.Write;

public class ParticipantAccountConfiguration : IEntityTypeConfiguration<ParticipantAccount>
{
    public void Configure(EntityTypeBuilder<ParticipantAccount> builder)
    {
        builder.ToTable("participant_accounts");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FavoritePets);
    }
}