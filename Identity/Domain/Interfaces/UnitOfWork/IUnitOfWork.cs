namespace Identity.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken);
}