using Template.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Template.Data.Base;

public abstract class RepositoryBase<TContext, TEntity> : IRepositoryBase<TEntity>
    where TEntity : EntityBase<TEntity>
    where TContext : DbContext
{
    private readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(TContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    protected virtual DbSet<TEntity> GetDbSet()
    {
        return _dbSet;
    }

    protected virtual IQueryable<TEntity> GetDbSetQuery()
    {
        return _dbSet?.AsNoTracking();
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync()
    {
        return await GetDbSetQuery().ToListAsync();
    }

    public async Task<TEntity> FindAsync(Guid key)
    {
        return await GetDbSet().FindAsync(key);
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(IEnumerable<Expression<Func<TEntity, bool>>> expressions)
    {
        var queryable = GetDbSetQuery();

        foreach (var expression in expressions)
        {
            queryable = queryable.Where(expression);
        }

        return await queryable.FirstOrDefaultAsync();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await GetDbSet().AddAsync(entity);
    }

    public virtual async Task AddListAsync(IEnumerable<TEntity> entities)
    {
        await GetDbSet().AddRangeAsync(entities);
    }

    public virtual Task UpdateListAsync(IEnumerable<TEntity> entities)
    {
        GetDbSet().UpdateRange(entities);

        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        GetDbSet().Update(entity);

        return Task.CompletedTask;
    }

    public virtual Task RemoveAsync(TEntity entity)
    {
        GetDbSet().Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        GetDbSet().RemoveRange(entities);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
