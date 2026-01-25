using System.Collections;
using EVDMS.Core.Base;
using EVDMS.DataAccessLayer.Database;
using EVDMS.DataAccessLayer.Repository.Abstraction;
using Microsoft.EntityFrameworkCore.Storage;

namespace EVDMS.DataAccessLayer.Repository.Implement;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Hashtable _repositories = new();
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        if (_transaction is not null)
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IGenericRepository<T, TId> GetRepository<T, TId>() where T : TIdentity<TId>
    {
        var type = typeof(T).Name;

        if (_repositories.ContainsKey(type))
            return (IGenericRepository<T, TId>)_repositories[type]!;

        var repositoryType = typeof(GenericRepository<,>);
        var repositoryInstance = Activator.CreateInstance(
            repositoryType.MakeGenericType(typeof(T), typeof(TId)),
            _context);

        _repositories.Add(type, repositoryInstance);

        return (IGenericRepository<T, TId>)repositoryInstance!;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null) return;
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction is null) throw new InvalidOperationException("Transaction has not been started.");


            await SaveChangesAsync(cancellationToken);


            await _transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction is not null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction is not null) await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            if (_transaction is not null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}