using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Data.EntityConfiguration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder
            .ToTable("user_tokens", "identity")
            .HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(x => x.LoginProvider)
            .HasColumnName("login_provider")
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        
        builder.Property(x => x.Value)
            .HasColumnName("value")
            .IsRequired();
        
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId); // Define a chave estrangeira para UserTokes
    }
}