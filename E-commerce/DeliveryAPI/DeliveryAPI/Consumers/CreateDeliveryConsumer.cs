using AutoMapper;
using DeliveryAPI.Domain.Entities;
using DeliveryAPI.Service.Interfaces;
using MassTransit;
using Rabbit.DTO;

namespace DeliveryAPI.Consumers
{
    public class CreateDeliveryConsumer : IConsumer<DeliveryRabbitDTO>
    {
        private readonly IDeliveryService _service;

        private readonly IMapper _mapper;

        public CreateDeliveryConsumer(IDeliveryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<DeliveryRabbitDTO> context)
        {
            var content = context.Message;
            var delivery = _mapper.Map<Delivery>(content);
            await _service.Create(delivery);
        }
    }
}
