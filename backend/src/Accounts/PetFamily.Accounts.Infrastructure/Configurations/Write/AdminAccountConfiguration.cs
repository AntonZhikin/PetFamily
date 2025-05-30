using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain.Accounts;

namespace PetFamily.Accounts.Infrastructure.Configurations.Write;

public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("admin_accounts");
        builder.HasKey(x => x.Id);
        // builder.ToTable("admin_accounts");
        // builder.HasOne(a => a.User)
        //     .WithOne()
        //     .HasForeignKey<AdminAccount>(a => a.UserId);
    }
}