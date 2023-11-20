using DeliveryAPI.Domain.Entities;

namespace DeliveryAPI.Service.Interfaces
{
    public interface ICourierService
    {
        Task<List<Courier>> GetAll();
        Task<Courier> GetById(Guid id);
        Task Create(Courier courier);
    }
}
