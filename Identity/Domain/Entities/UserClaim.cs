using Identity.Domain.Entities.Base;

namespace Identity.Domain.Entities;

public class UserClaim : EntityBase
{
    public int UserId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }

    // Navigation Property for User
    public User User { get; set; }
}