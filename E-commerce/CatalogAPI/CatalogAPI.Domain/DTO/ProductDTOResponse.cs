using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPI.Domain.DTO
{
    public class ProductDTOResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public bool IsSale { get; set; }
        public CategoryDTOResponse? Category { get; set; }

        public BrandDTOResponse? Brand { get; set; }
    }
}
