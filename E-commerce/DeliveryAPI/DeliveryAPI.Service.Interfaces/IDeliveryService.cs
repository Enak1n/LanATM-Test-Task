using DeliveryAPI.Domain.Entities;

namespace DeliveryAPI.Service.Interfaces
{
    public interface IDeliveryService
    {
        Task<List<Delivery>> GetAll();
        Task<Delivery> GetById(Guid id);
        Task<Delivery> Create(Delivery delivery);
        Task PickUpOrderFromWarehouse(Guid orderId, Guid courierId);
        Task Complete(Guid id);
        Task Cancel(Guid id);
        Task ReturnToWarehouse(Guid orderId);
    }
}
