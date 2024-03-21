using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Data.EntityConfiguration;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder
            .ToTable("user_logins", "identity")
            .HasKey(x => x.LoginProvider);

        builder.Property(x => x.LoginProvider)
            .HasColumnName("login_provider")
            .IsRequired();
        
        builder.Property(x => x.ProviderKey)
            .HasColumnName("provider_key")
            .IsRequired();
        
        builder.Property(x => x.ProviderDisplayName)
            .HasColumnName("provider_display_name")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

    
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId); // Define a chave estrangeira para UserToken
    }
}