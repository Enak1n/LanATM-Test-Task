using OrderAPI.Domain.Entities;
using OrderAPI.Domain.Exceptions;
using OrderAPI.Domain.Interfaces;
using OrderAPI.Service.Interfaces;

namespace OrderAPI.Service.Business
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> Create(Address address)
        {
            await _unitOfWork.Addresses.AddAsync(address);

            return address;
        }

        public async Task<List<Address>> GetAll()
        {
            return await _unitOfWork.Addresses.GetAllAsync();
        }

        public async Task<Address> GetById(Guid id)
        {
            var order = await _unitOfWork.Addresses.GetByIdAsync(id);

            if (order == null)
            {
                throw new NotFoundException($"Address with ID {id} not found!");
            }

            return order;
        }
    }
}
