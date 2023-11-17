using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Domain.DTO.Requests
{
    public class OrderDTORequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid AddressId { get; set; }
        public double TotalValue { get; set; }
    }
}
