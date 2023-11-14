using System.Linq.Expressions;

namespace CatalogAPI.Domain.Intefaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        TEntity GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TEntity entity);
        Task EditAsync(TEntity entity);
    }
}
