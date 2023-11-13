using IdentityAPI.Domain.Entities;
using System.Linq.Expressions;

namespace IdentityAPI.Interfaces 
{ 
    public interface IGenericRepository<T> where T : class
    {
        Task AddRange(IEnumerable<T> entities);
        Task Add(T entity);
        Task<List<Token>> GetTokensByUserId(Guid userId);
        Task<T> Find(Expression<Func<T, bool>> predicate);
    }
}
