using Identity.Domain.Entities.Base;

namespace Identity.Domain.Entities;

public class UserToken : EntityBase
{
    public int UserId { get; set; }
    public string LoginProvider { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }

    // Navigation Property for User
    public User User { get; set; }
}