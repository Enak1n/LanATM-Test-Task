using IdentityAPI.Domain.Entities;
using IdentityAPI.Infrastructure.DataBase;
using IdentityAPI.Interfaces;

namespace IdentityAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }
    }
}
