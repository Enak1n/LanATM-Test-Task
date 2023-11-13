using IdentityAPI.Domain.Entities;
using IdentityAPI.Infrastructure.DataBase;
using IdentityAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdentityAPI.Repositories
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

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _dataBase.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _dataBase.AddAsync(entity);
        }

        public async Task<List<Token>> GetTokensByUserId(Guid userId)
        {
            return await _context.Tokens.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataBase.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
