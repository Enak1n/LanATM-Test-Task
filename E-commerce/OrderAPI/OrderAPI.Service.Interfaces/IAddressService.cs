using OrderAPI.Domain.Entities;

namespace OrderAPI.Service.Interfaces
{
    public interface IAddressService
    {
        List<Order> GetAll();
        Order GetById(Guid id);
        Task<Order> Create(Order order);
    }
}
