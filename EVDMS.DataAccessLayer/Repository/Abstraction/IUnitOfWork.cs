using EVDMS.Core.Base;

namespace EVDMS.DataAccessLayer.Repository.Abstraction;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T, TId> GetRepository<T, TId>() where T : TIdentity<TId>;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}