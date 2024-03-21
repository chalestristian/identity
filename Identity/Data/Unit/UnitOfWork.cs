using Identity.Domain.Interfaces.UnitOfWork;
using Identity.Infra.Data.Context;

namespace Data.Unit;

public class UnitOfWork : IUnitOfWork
{
    private readonly IdentityDbContext _context;

    public UnitOfWork(IdentityDbContext context)
        => _context = context;

    public async Task SaveAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);
}