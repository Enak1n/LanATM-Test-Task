namespace OrderAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        public double TotalValue { get; set; }
        public bool IsReady { get; set; } = false;
        public bool IsReceived { get; set; } = false;
        public bool IsCanceled { get; set; } = false;
        public bool IsPaymented { get; set; } = false;
        public Address? Address { get; set; }
    }
}
