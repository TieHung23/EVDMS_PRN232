using System.Linq.Expressions;
using EVDMS.Core.Base;
using EVDMS.DataAccessLayer.Database;
using EVDMS.DataAccessLayer.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DataAccessLayer.Repository.Implement;

public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : TIdentity<TId>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(TId id)
    {
        var result = await _dbContext.Set<T>().FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (!string.IsNullOrEmpty(includeProperties))
            query = includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (filter != null) query = query.Where(filter);

        if (orderBy != null) return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public Task AddAsync(T entity)
    {
        var result = _dbContext.Set<T>().AddAsync(entity);
        return result.AsTask();
    }

    public bool Update(T entity)
    {
        if (entity is IModifiable modified)
        {
            modified.ModifiedAt = DateTime.Now;
            modified.ModifiedAtTick = DateTime.Now.Ticks.ToString();
        }

        _ = _dbContext.Set<T>().Update(entity);
        return true;
    }

    public bool Delete(T entity)
    {
        if (entity is IStatus activated and IModifiable modified)
        {
            activated.IsActive = false;
            activated.IsDeleted = true;
            modified.ModifiedAt = DateTime.Now;
            modified.ModifiedAtTick = DateTime.Now.Ticks.ToString();
            _ = _dbContext.Set<T>().Update(entity);
        }
        else
        {
            _dbContext.Set<T>().Remove(entity);
        }

        return true;
    }

    public Task AddRangeAsync(IEnumerable<T> entities)
    {
        var result = _dbContext.Set<T>().AddRangeAsync(entities);
        return Task.FromResult(result);
    }

    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string includeProperties = "", bool disableTracking = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrEmpty(includeProperties))
            query = includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (filter != null) query = query.Where(filter);

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<IEnumerable<T>> GetFilterAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "",
        bool disableTracking = true,
        int skip = 0,
        int take = 0, CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrEmpty(includeProperties))
            query = includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (filter != null) query = query.Where(filter);

        if (skip > 0) query = query.Skip(skip);

        if (take > 0) query = query.Take(take);

        return Task.FromResult(orderBy != null ? orderBy(query).AsEnumerable() : query.AsEnumerable());
    }
}