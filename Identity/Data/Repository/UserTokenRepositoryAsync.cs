using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repository;
using Identity.Infra.Data.Context;
using Identity.Infra.Data.Repository.RepositoryBase;

namespace Identity.Infra.Data.Repository;

public class UserTokenRepositoryAsync : RepositoryBaseAsync<UserToken>, IUserTokenRepositoryAsync
{    
    public UserTokenRepositoryAsync(IdentityDbContext context) : base(context){}
}