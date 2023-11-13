using CatalogAPI.Domain.Entities;

namespace CatalogAPI.Service.Intefaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(Guid id);
        Task<Category> GetByName(string name);
        Task Create(Category category);
        Task Update(Category category);
    }
}
