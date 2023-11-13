using IdentityAPI.Domain.Entities;
using IdentityAPI.Infrastructure.DataBase;
using IdentityAPI.Interfaces;

namespace IdentityAPI.Repositories
{
    public class TokenRepository : GenericRepository<Token>, ITokenRepository
    {
        public TokenRepository(Context context) : base(context) { }
    }
}
