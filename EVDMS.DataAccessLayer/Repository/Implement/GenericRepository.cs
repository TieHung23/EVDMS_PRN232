using System.Linq.Expressions;
using EVDMS.Core.Base;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.DataAccessLayer.Repository.Implement;

public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : TIdentity<TId>
{
    public Task<T?> GetByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Update(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "", bool disableTracking = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetFilterAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "",
        bool disableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}