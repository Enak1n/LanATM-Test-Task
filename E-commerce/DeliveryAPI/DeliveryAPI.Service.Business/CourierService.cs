using DeliveryAPI.Domain.Entities;
using DeliveryAPI.Domain.Exceptions;
using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Service.Interfaces;

namespace DeliveryAPI.Service.Business
{
    public class CourierService : ICourierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Courier courier)
        {
            await _unitOfWork.Couriers.AddAsync(courier);
        }

        public async Task<List<Courier>> GetAll()
        {
            return await _unitOfWork.Couriers.GetAllAsync();
        }

        public Task<Courier> GetById(Guid id)
        {
            var result = _unitOfWork.Couriers.GetByIdAsync(id);

            if (result == null)
            {
                throw new NotFoundException($"Courier with Id {id} not found!");
            }

            return result;
        }
    }
}
