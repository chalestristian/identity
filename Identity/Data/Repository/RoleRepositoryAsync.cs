using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repository;
using Identity.Infra.Data.Context;
using Identity.Infra.Data.Repository.RepositoryBase;

namespace Identity.Infra.Data.Repository;

public class RoleRepositoryAsync : RepositoryBaseAsync<Role>, IRoleRepositoryAsync
{    
    public RoleRepositoryAsync(IdentityDbContext context) : base(context){}
}