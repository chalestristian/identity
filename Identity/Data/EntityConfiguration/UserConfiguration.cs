using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Data.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder
            .ToTable("Users")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");
        
        builder.Property(x => x.UserName)
            .HasColumnName("UserName");
        
        builder.Property(x => x.NormalizedUserName)
            .HasColumnName("NormalizedUserName");
        
        builder.Property(x => x.Email)
            .HasColumnName("Email");

        builder.Property(x => x.NormalizedEmail)
            .HasColumnName("NormalizedEmail");

        builder.Property(x => x.EmailConfirmed)
            .HasColumnName("EmailConfirmed");

        builder.Property(x => x.PasswordHash)
            .HasColumnName("PasswordHash");

        builder.Property(x => x.SecurityStamp)
            .HasColumnName("SecurityStamp");

        builder.Property(x => x.ConcurrencyStamp)
            .HasColumnName("ConcurrencyStamp");

        builder.Property(x => x.PhoneNumber)
            .HasColumnName("PhoneNumber");
        builder.Property(x => x.PhoneNumberConfirmed)
            .HasColumnName("PhoneNumberConfirmed");

        builder.Property(x => x.TwoFactorEnabled)
            .HasColumnName("TwoFactorEnabled");

        builder.Property(x => x.LockoutEnd)
            .HasColumnName("LockoutEnd");

        builder.Property(x => x.AccessFailedCount)
            .HasColumnName("AccessFailedCount");

        /*builder
            .HasMany(x => x.Claims)
            .WithOne()
            .HasForeignKey(x => x.UserId); // Define a chave estrangeira para UserClaim

        builder
            .HasMany(x => x.Logins)
            .WithOne()
            .HasForeignKey(x => x.UserId); // Define a chave estrangeira para UserLogin

        builder
            .HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(x => x.UserId); // Define a chave estrangeira para UserRole

        builder
            .HasMany(x => x.Tokens)
            .WithOne()
            .HasForeignKey(x => x.UserId); // Define a chave estrangeira para UserToken*/
    }
}