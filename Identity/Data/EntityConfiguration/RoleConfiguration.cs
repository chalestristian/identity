using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Data.EntityConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<string>> builder)
    {
        builder
            .ToTable("Role");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired();
        
        builder.Property(x => x.NormalizedName)
            .HasColumnName("NormalizedName")
            .IsRequired();

        builder.Property(x => x.ConcurrencyStamp)
            .HasColumnName("ConcurrencyStamp")
            .IsRequired();
        
        
        // builder
        //     .HasMany(x => x.Claims)
        //     .WithOne()
        //     .HasForeignKey(x => x.RoleId); // Define a chave estrangeira
    }
}