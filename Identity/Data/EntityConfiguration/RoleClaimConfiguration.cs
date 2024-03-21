using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Data.EntityConfiguration;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder
            .ToTable("role_claims", "identity")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();
        
        builder.Property(x => x.RoleId)
            .HasColumnName("role_id")
            .IsRequired();
        
        builder.Property(x => x.ClaimType)
            .HasColumnName("claim_type")
            .IsRequired();
        
        builder.Property(x => x.ClaimValue)
            .HasColumnName("claim_value")
            .IsRequired();
        
        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId); // Define a chave estrangeira
    }
}