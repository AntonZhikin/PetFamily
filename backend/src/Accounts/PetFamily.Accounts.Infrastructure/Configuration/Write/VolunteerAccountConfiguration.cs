using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.Accounts;

namespace PetFamily.Accounts.Infrastructure.Configuration.Write;

public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExperienceYear);

        /*builder.OwnsOne(r => r.AssistanceDetails, rb =>
        {
            rb.ToJson("assistance_details");

            rb.OwnsMany(s => s.AssistanceDetails, rf =>
            {
                rf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
                rf.Property(g => g.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            });
        });*/
    }
}