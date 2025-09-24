using System.Linq.Expressions;

namespace Template.Domain.Base
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase<TEntity>
    {
        Task<IEnumerable<TEntity>> FindAllAsync();
        Task<TEntity> FindAsync(Guid key);
        Task<TEntity> FirstOrDefaultAsync(IEnumerable<Expression<Func<TEntity, bool>>> expressions);
        Task AddAsync(TEntity entity);
        Task AddListAsync(IEnumerable<TEntity> entities);
        Task UpdateListAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}
