using CatalogAPI.Domain.Entities;

namespace CatalogAPI.Service.Intefaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();
        Task<Brand> GetById(Guid id);
        Task<Brand> GetByName(string name);
        Task Create(Brand brand);
        Task Update(Brand brand);
    }
}
