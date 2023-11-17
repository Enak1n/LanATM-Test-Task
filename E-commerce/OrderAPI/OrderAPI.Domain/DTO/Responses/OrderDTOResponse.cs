namespace OrderAPI.Domain.DTO.Responses
{
    public class OrderDTOResponse
    {
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        public double TotalValue { get; set; }
        public bool IsReady { get; set; }
        public bool IsReceived { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsPaymented { get; set; }
    }
}
