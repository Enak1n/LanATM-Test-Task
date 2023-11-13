using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Domain.DTO
{
    public class ProductDTORequest
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}
