using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogAPI.Domain.DTO
{
    public class BrandDTORequest
    {
        [Required]
        public string Name { get; set; }
        
        public string? Description { get; set; }
    }
}
