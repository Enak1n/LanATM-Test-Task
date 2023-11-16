using OrderAPI.Domain.Entities;
using OrderAPI.Service.Interfaces;

namespace OrderAPI.Service.Business
{
    public class OrderService : IOrderService
    {
        public Task<Order> Cancel(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Create(Order order)
        {
            throw new NotImplementedException();
        }

        public Task CreateDelivery(Order order, Guid addressId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> IsPaymented(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> IsReady(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> IsReceived(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
