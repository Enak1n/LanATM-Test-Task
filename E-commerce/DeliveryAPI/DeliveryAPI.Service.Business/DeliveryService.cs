using DeliveryAPI.Domain.Entities;
using DeliveryAPI.Domain.Exceptions;
using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Service.Interfaces;

namespace DeliveryAPI.Service.Business
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Cancel(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Complete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Delivery> Create(Delivery delivery)
        {
            await _unitOfWork.Deliveries.AddAsync(delivery);

            return delivery;
        }

        public async Task<List<Delivery>> GetAll()
        {
            return await _unitOfWork.Deliveries.GetAllAsync();
        }

        public async Task<Delivery> GetById(Guid id)
        {
            var delivery = await _unitOfWork.Deliveries.GetByIdAsync(id);

            if (delivery == null)
                throw new NotFoundException($"Delivery with Id {id} not found!");

            return delivery;
        }

        public Task PickUpOrderFromWarehouse(Guid orderId, Guid courierId)
        {
            throw new NotImplementedException();
        }

        public Task ReturnToWarehouse(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
