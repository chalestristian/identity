using Identity.Domain.Entities.Base;

namespace Identity.Domain.Entities;

public class UserRole : EntityBase
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    // Navigation Property for User
    public User User { get; set; }

    // Navigation Property for Role
    public Role Role { get; set; }
}