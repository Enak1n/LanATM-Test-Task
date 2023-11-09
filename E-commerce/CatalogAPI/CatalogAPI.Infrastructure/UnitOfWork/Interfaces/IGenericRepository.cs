using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPI.Infrastructure.UnitOfWork.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TEntity entity);
        Task EditAsync(TEntity entity);
    }
}
