using Identity.Domain.Entities.Base;

namespace Identity.Domain.Entities;

public class User : EntityBase
{
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }

    // Navigation Property for User Claims
    public ICollection<UserClaim> Claims { get; set; }

    // Navigation Property for User Logins
    public ICollection<UserLogin> Logins { get; set; }

    // Navigation Property for User Roles
    public ICollection<UserRole> Roles { get; set; }

    // Navigation Property for User Tokens
    public ICollection<UserToken> Tokens { get; set; }
}