using AutoMapper;
using DeliveryAPI.Domain.Entities;
using DeliveryAPI.Service.Interfaces;
using MassTransit;
using Rabbit.DTO;

namespace DeliveryAPI.Consumers
{
    public class CreateCourierConsumer : IConsumer<CourierRabbitDTO>
    {
        private readonly ICourierService _service;
        private readonly IMapper _mapper;

        public CreateCourierConsumer(ICourierService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CourierRabbitDTO> context)
        {
            var content = context.Message;
            var courier = _mapper.Map<Courier>(content);
            await _service.Create(courier);
        }
    }
}
