using System.Linq.Expressions;
namespace Identity.Domain.Interfaces.Repository.Base;

public interface IGenericRepositoryBaseAsync<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task AddAsync(T obj, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> obj, CancellationToken cancellationToken);
    void Update(T obj);
    void Delete(T obj);
    void DeleteRange(IEnumerable<T> obj);
}