using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repository;
using Identity.Infra.Data.Context;
using Identity.Infra.Data.Repository.RepositoryBase;

namespace Identity.Infra.Data.Repository;

public class UserClaimRepositoryAsync : RepositoryBaseAsync<UserClaim>, IUserClaimRepositoryAsync
{    
    public UserClaimRepositoryAsync(IdentityDbContext context) : base(context){}
}