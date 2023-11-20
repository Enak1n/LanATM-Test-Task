using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAPI.Domain.Entities
{
    public class Courier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DeliveryId { get; set; }

        public List<Delivery> Deliveries { get; set; } = new();
    }
}
