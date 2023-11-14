using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Exceptions;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Service.Intefaces;

namespace CatalogAPI.Infrastructure.Business
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Brand brand)
        {
            if (await _unitOfWork.Brands.FindAsync(x => x.Name == brand.Name) != null)
            {
                throw new UniqueException($"Value with name {brand.Name} already exist!");
            }

            await _unitOfWork.Brands.AddAsync(brand);
        }

        public async Task<List<Brand>> GetAll()
        {
            return await _unitOfWork.Brands.GetAllAsync();
        }

        public async Task<Brand> GetById(Guid id)
        {
            var brand = _unitOfWork.Brands.GetByIdAsync(id);

            if (brand == null)
                throw new NotFoundException($"Brand with Id {id} not found!");

            return brand;
        }

        public async Task<Brand> GetByName(string name)
        {
            var brand = await _unitOfWork.Brands.FindAsync(x => x.Name == name);

            if (brand == null)
                throw new NotFoundException($"Brand with name {name} not found!");

            return brand;
        }

        public async Task Update(Brand brand)
        {
            var res = _unitOfWork.Brands.GetByIdAsync(brand.Id);

            if (res == null)
                throw new NotFoundException($"Brand wasn't found!");

            if (await _unitOfWork.Brands.FindAsync(x => x.Name == brand.Name) != null && brand.Name != brand.Name)
            {
                throw new UniqueException($"Value with name {brand.Name} already exist!");
            }

            await _unitOfWork.Brands.EditAsync(brand);
        }
    }
}
