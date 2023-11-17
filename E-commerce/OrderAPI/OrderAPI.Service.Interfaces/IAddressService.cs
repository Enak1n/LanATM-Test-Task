using OrderAPI.Domain.Entities;

namespace OrderAPI.Service.Interfaces
{
    public interface IAddressService
    {
        Task<List<Address>> GetAll();
        Task<Address> GetById(Guid id);
        Task<Address> Create(Address address);
    }
}
