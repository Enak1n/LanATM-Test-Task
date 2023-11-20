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

        public async Task Cancel(Guid id)
        {
            var delivery = await _unitOfWork.Deliveries.GetByIdAsync(id);

            if (delivery == null)
                throw new NotFoundException($"Delivery with Id {id} not found!");

            delivery.IsCancel = true;
        }

        public async Task Complete(Guid id)
        {
            var delivery = await _unitOfWork.Deliveries.GetByIdAsync(id);

            if (delivery == null)
                throw new NotFoundException($"Delivery with Id {id} not found!");

            delivery.IsComplete = true;
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

        public async Task PickUpOrderFromWarehouse(Guid id, Guid courierId)
        {
            var delivery = await _unitOfWork.Deliveries.GetByIdAsync(id);

            if (delivery == null)
            {
                throw new NotFoundException($"Delivery with this Id {id} not found!");
            }

            delivery.CourierId = courierId;
        }

        public Task ReturnToWarehouse(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
