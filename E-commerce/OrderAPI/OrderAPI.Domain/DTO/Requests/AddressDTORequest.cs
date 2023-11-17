using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Domain.DTO.Requests
{
    public class AddressDTORequest
    {
        [Required]
        public string? City { get; set; }

        [Required]
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
