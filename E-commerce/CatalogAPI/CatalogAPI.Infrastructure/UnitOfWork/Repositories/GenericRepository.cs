using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
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
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TEntity entity)
        {
            await Task.Run(() => _dataBase.Update(entity));
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataBase.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _dataBase.AsNoTracking().ToListAsync();
            return entities;
        }

        public TEntity GetByIdAsync(Guid id)
        {
            var entity = _dataBase.Find(id);

            if(entity == null)
            {
                return null;
            }

            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() => _dataBase.Remove(entity));
            await _context.SaveChangesAsync();
        }
    }
}
