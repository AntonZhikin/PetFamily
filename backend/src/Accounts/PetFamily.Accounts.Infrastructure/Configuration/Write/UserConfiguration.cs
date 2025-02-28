using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Accounts.Domain;
using PetFamily.Accounts.Domain.Accounts;
using PetFamily.Core;
using PetFamily.Kernel;

namespace PetFamily.Accounts.Infrastructure.Configuration.Write;

public class UserConfiguration :  IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        // builder
        //     .HasMany(u => u.Roles)
        //     .WithMany()
        //     .UsingEntity<IdentityUserRole<Guid>>();
        
        builder.Property(r => r.RoleId)
            .HasConversion(
                i => i.Value,
                value => RoleId.Create(value).Value)
            .IsRequired()
            .HasColumnName("role_id");
        
        builder.HasOne(d => d.Role)
            .WithMany();
        
        builder.HasOne(u => u.Admin)
            .WithOne(u => u.User)
            .HasForeignKey<AdminAccount>(d => d.UserId)
            .IsRequired(false);

        builder.HasOne(u => u.Participant)
            .WithOne(u => u.User)
            .HasForeignKey<ParticipantAccount>(d => d.UserId)
            .IsRequired(false);

        builder.HasOne(u => u.Volunteer)
            .WithOne(u => u.User)
            .HasForeignKey<VolunteerAccount>(d => d.UserId)
            .IsRequired(false);
        
        builder.Property(f => f.Email)
            .IsRequired();
    }
    
}