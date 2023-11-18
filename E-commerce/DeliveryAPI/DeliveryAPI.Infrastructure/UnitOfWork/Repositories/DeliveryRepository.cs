using DeliveryAPI.Domain.Entities;
using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Infrastructure.DataBase;

namespace DeliveryAPI.Infrastructure.UnitOfWork.Repositories
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(Context context) : base(context) { }
    }
}
