using AutoMapper;
using CatalogAPI.Domain.DTO;
using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Exceptions;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Service.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, ICategoryService categoryService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Categories.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _categoryService.GetById(id);
                var response = _mapper.Map<CategoryDTOResponse>(res);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var res = await _categoryService.GetByName(name);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTORequest model)
        {
            try
            {
                Category product = _mapper.Map<Category>(model);

                await _categoryService.Create(product);
                await _unitOfWork.SaveChangesAsync();

                return Ok("Category success created!");
            }
            catch (UniqueException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, CategoryDTORequest model)
        {
            try
            {
                Category category = _mapper.Map<Category>(model);
                category.Id = id;

                await _categoryService.Update(category);
                await _unitOfWork.SaveChangesAsync();

                return Ok("You updated your category!");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UniqueException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
