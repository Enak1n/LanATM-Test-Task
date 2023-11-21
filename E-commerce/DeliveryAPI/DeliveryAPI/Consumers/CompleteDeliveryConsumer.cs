using AutoMapper;
using DeliveryAPI.Domain.Exceptions;
using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Service.Interfaces;
using MassTransit;
using Rabbit.DTO;

namespace DeliveryAPI.Consumers
{
    public class CompleteDeliveryConsumer : IConsumer<DeliveryRabbitDTO>
    {
        private readonly IDeliveryService _service;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<CompleteDeliveryConsumer> _logger;

        public CompleteDeliveryConsumer(IDeliveryService service, IMapper mapper, IUnitOfWork unitOfWork,
                                        ILogger<CompleteDeliveryConsumer> logger)
        {
            _service = service;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DeliveryRabbitDTO> context)
        {
            var content = context.Message;

            var delivery = await _unitOfWork.Deliveries.GetByIdAsync(content.OrderId);

            if (delivery == null)
                throw new NotFoundException($"Delivery with id {content.OrderId} not found!");

            delivery.IsComplete = true;

            _logger.LogInformation($"{delivery.Id}");

            await _unitOfWork.Deliveries.EditAsync(delivery);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
