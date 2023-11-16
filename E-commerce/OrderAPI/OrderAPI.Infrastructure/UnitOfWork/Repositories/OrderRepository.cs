using OrderAPI.Domain.Entities;
using OrderAPI.Domain.Interfaces;
using OrderAPI.Infrastructure.DataBase;

namespace OrderAPI.Infrastructure.UnitOfWork.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(Context context) : base(context)
        {
        }
    }
}
