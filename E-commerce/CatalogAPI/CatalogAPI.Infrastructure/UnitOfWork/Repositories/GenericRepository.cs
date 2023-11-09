using CatalogAPI.Infrastructure.DataBase;
using CatalogAPI.Infrastructure.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace CatalogAPI.Infrastructure.UnitOfWork.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;

        protected readonly DbSet<TEntity> _dataBase;

        public GenericRepository(Context context)
        {
            _context = context;
            _dataBase = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dataBase.AddAsync(entity);
        }

        public async Task EditAsync(TEntity entity)
        {
            _dataBase.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataBase.Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _dataBase.AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dataBase.FindAsync(id);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() => _dataBase.Remove(entity));
        }
    }
}
