using OrderAPI.Domain.Entities;

namespace OrderAPI.Service.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order GetById(Guid id);
        Task<Order> Create(Order order);
        Task<Order> IsReady(Guid id);
        Task<Order> IsReceived(Guid id);
        Task<Order> Cancel(Guid id);
        Task<Order> IsPaymented(Guid id);
        Task CreateDelivery(Order order, Guid addressId);
    }
}
