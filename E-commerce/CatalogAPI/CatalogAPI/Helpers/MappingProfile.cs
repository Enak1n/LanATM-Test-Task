using AutoMapper;
using CatalogAPI.Domain.DTO;
using CatalogAPI.Domain.Entities;

namespace CatalogAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BrandDTORequest, Brand>();

            CreateMap<Brand, BrandDTOResponse>();

            CreateMap<CategoryDTORequest, Category>();

            CreateMap<Category, CategoryDTOResponse>();

            CreateMap<ProductDTORequest, Product>();

            CreateMap<Product, ProductDTOResponse>();
        }
    }
}
