using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Data.EntityConfiguration;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder
            .ToTable("user_claims", "identity")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(x => x.ClaimType)
            .HasColumnName("claim_type")
            .IsRequired();
        
        builder.Property(x => x.ClaimValue)
            .HasColumnName("claim_value")
            .IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId); // Define a chave estrangeira para UserClaim
      
    }
}