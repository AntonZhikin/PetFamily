using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Infrastructure.Configuration.Write;

public class UserConfiguration :  IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        //builder.HasKey(x => x.Id);
        
        builder
            .HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();

        /*builder.ComplexProperty(u => u.FullName, gf =>
        {
            gf.Property(n => n.Name)
                .HasColumnName("name")
                .IsRequired();
            gf.Property(n => n.Surname)
                .HasColumnName("surname")
                .IsRequired();
            gf.Property(n => n.SecondName)
                .HasColumnName("second_name")
                .IsRequired();
        });*/

        builder.Property(f => f.Email)
            .IsRequired();
        
        /*builder.OwnsOne(s => s.SocialNetworkList, sb => 
        {
            sb.ToJson();

            sb.OwnsMany(s => s.SocialNetworks, sf =>
            {
                sf.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
                sf.Property(g => g.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGHT);
            });
        });*/
    }
    
}