using System.Linq.Expressions;
using EVDMS.Core.Base;

namespace EVDMS.DataAccessLayer.Repository.Abstraction;

public interface IGenericRepository<T, in TId> where T : TIdentity<TId>
{
    Task<T?> GetByIdAsync(TId id);

    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = ""
    );

    Task AddAsync(T entity);
    bool Update(T entity);
    bool Delete(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);

    Task<T?> GetFirstOrDefaultAsync(
        Expression<Func<T, bool>>? filter = null,
        string includeProperties = "",
        bool disableTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> GetFilterAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "",
        bool disableTracking = true,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default);
}