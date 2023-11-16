using OrderAPI.Domain.Entities;
using OrderAPI.Domain.Interfaces;
using OrderAPI.Infrastructure.DataBase;

namespace OrderAPI.Infrastructure.UnitOfWork.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(Context context) : base(context)
        {
        }
    }
}
