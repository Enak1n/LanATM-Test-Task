using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Service.Intefaces;
using CatalogAPI.Domain.Exceptions;
using System.Xml.Linq;

namespace CatalogAPI.Infrastructure.Business
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Category category)
        {
            if (await _unitOfWork.Categories.FindAsync(x => x.Name == category.Name) != null)
            {
                throw new UniqueException($"Category with name {category.Name} already exist!");
            }

            await _unitOfWork.Categories.AddAsync(category);
        }

        public async Task Delete(Guid id)
        {
            var category = _unitOfWork.Categories.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException($"Category with Id {id} not found!");

            await _unitOfWork.Categories.RemoveAsync(category);
        }

        public async Task<List<Category>> GetAll()
        {
            return (await _unitOfWork.Categories.GetAllAsync());
        }

        public async Task<Category> GetById(Guid id)
        {
            var res = await _unitOfWork.Categories.FindAsync(x => x.Id == id);

            if (res == null)
                throw new NotFoundException($"Category with id {id} not found!");

            return res;
        }

        public async Task<Category> GetByName(string name)
        {
            var res = await _unitOfWork.Categories.FindAsync(x => x.Name == name);

            if (res == null)
                throw new NotFoundException($"Category with name {name} not found!");

            return res;
        }

        public async Task Update(Category category)
        {

            var res = _unitOfWork.Categories.GetByIdAsync(category.Id);

            if (res == null)
            {
                throw new NotFoundException($"Category with Id {category.Id} not found!");
            }

             if (await _unitOfWork.Categories.FindAsync(x => x.Name == category.Name) != null)
            {
                throw new UniqueException($"Category with name {category.Name} already exists!");
            }

            await _unitOfWork.Categories.EditAsync(category);
        }
    }
}
