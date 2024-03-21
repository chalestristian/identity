using Identity.Domain.Entities.Base;

namespace Identity.Domain.Entities;

public class RoleClaim : EntityBase
{
    public int RoleId { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }

    // Navigation Property for Role
    public Role Role { get; set; }
}