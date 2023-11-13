using IdentityAPI.Infrastructure.DataBase;
using IdentityAPI.Interfaces;
using IdentityAPI.Repositories;

namespace IdentityAPI.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public ITokenRepository Tokens { get; private set; }

        public IUserRepository Users { get; private set; }

        public IAddressRepository Addresses { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
            Tokens = new TokenRepository(context);
            Users = new UserRepository(context);
            Addresses = new AddressRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
