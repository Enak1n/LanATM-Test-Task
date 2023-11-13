using IdentityAPI.Domain.Entities;
using IdentityAPI.Infrastructure.DataBase;
using IdentityAPI.Interfaces;

namespace IdentityAPI.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(Context context) : base(context) { }
    }
}
