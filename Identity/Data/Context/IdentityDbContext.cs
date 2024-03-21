using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infra.Data.Context;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {}

    public DbSet<Role> Role1 { get; set; }
    public DbSet<RoleClaim> RoleClaim { get; set; }
    public DbSet<User> User1 { get; set; }
    public DbSet<UserClaim> UserClaim1 { get; set; }
    public DbSet<UserLogin> UserLogin { get; set; }
    public DbSet<UserRole> UserRole1 { get; set; }
    public DbSet<UserToken> UserToken { get; set; }
    
    //AUTH
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityUserClaim<string>> UserClaim { get; set; }
    public DbSet<IdentityUserRole<string>> UserRole { get; set; }
    public DbSet<IdentityRole<string>> Role { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>().HasKey(m => m.UserId);
        builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });

        builder.Entity<IdentityRole<string>>().HasKey(m => m.Id);
        
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
    
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Port=3306;Database=identity;Uid=chalestristian;Pwd=senha;",
            b => b.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName));
    }*/

}