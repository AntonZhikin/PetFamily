using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.DTOs;

namespace PetFamily.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");
        
        builder.HasKey(x => x.Id);

        builder.HasMany(i => i.Pets)
            .WithOne()
            .HasForeignKey(x => x.VolunteerId);
    }
}