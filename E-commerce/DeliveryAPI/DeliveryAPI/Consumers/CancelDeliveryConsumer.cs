using DeliveryAPI.Domain.Exceptions;
using DeliveryAPI.Service.Interfaces;
using MassTransit;
using Rabbit.DTO;

namespace DeliveryAPI.Consumers
{
    public class CancelDeliveryConsumer : IConsumer<CancelDeliveryRabbitDTO>
    {
        private readonly IDeliveryService _service;

        public CancelDeliveryConsumer(IDeliveryService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<CancelDeliveryRabbitDTO> context)
        {
            try
            {
                var content = context.Message;
                _service.Cancel(content.OrderId);
            }
            catch (NotFoundException) { }
        }
    }
}
