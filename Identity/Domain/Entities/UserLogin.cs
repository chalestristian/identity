using Identity.Domain.Entities.Base;

namespace Identity.Domain.Entities;

public class UserLogin : EntityBase
{
    public string LoginProvider { get; set; }
    public string ProviderKey { get; set; }
    public string ProviderDisplayName { get; set; }
    public int UserId { get; set; }

    // Navigation Property for User
    public User User { get; set; }
}