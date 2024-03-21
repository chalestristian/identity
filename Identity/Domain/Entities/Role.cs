using Identity.Domain.Entities.Base;

namespace Identity.Domain.Entities;

public class Role : EntityBase
{
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        // Navigation Property for Role Claims
        public ICollection<RoleClaim> Claims { get; set; }
    
}