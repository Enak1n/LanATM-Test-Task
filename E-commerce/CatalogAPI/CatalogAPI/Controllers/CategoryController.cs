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
    [Produces("application/json")]
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

        /// <summary>
        /// Get all categories from data base
        /// </summary>
        /// <returns>Status response</returns>
        /// <response code="200">Returns the list of all brands</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Categories.GetAllAsync());
        }

        /// <summary>
        /// Get category by id from data base
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Status about getting category</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="400">Return meesage with error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Get category by name from Data base
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Status about getting category</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="400">Return meesage with error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Create a new brand
        /// </summary>
        /// <param name="model">Brand</param>
        /// <returns>Status about creating category</returns>
        /// <response code="200">Return message about success</response>
        /// <response code="400">Return error message while creating category</response> 
        /// <response code="409">Category with some properties already exist</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updare existed category
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">New category</param>
        /// <returns>Status about updating category</returns>
        /// <response code="200">Return message about success</response>
        /// <response code="404">Category was not foud in data base</response> 
        /// <response code="409">Category with some properties already exist</response> 
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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
