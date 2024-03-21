using System.Linq.Expressions;
using Identity.Domain.Entities.Base;
using Identity.Domain.Interfaces.Repository.Base;
using Identity.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infra.Data.Repository.RepositoryBase;

public class RepositoryBaseAsync<T> : IGenericRepositoryBaseAsync<T> where T : EntityBase
{
    protected readonly DbSet<T> _entity;

    public RepositoryBaseAsync(IdentityDbContext context)
    {
        _entity = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        => await _entity.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _entity.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        => await _entity.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

    public async Task AddAsync(T obj, CancellationToken cancellationToken)
    {
        await _entity.AddAsync(obj, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> obj, CancellationToken cancellationToken)
        => await _entity.AddRangeAsync(obj, cancellationToken);

    public void Update(T obj)
    {
        _entity.Update(obj);
    }

    public void Delete(T obj)
        => _entity.Remove(obj);

    public void DeleteRange(IEnumerable<T> obj)
        => _entity.RemoveRange(obj);
}