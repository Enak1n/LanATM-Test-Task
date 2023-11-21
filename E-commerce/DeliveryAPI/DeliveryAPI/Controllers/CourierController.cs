using AutoMapper;
using DeliveryAPI.Domain.Exceptions;
using DeliveryAPI.Domain.Interfaces.Repositories;
using DeliveryAPI.Service.Business;
using DeliveryAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = "ChangingOfDeliveries")]
    [Produces("application/json")]
    public class CourierController : ControllerBase
    {
        private readonly ICourierService _courierService;
        private readonly IMapper _mapper;

        public CourierController(ICourierService courierService, IMapper mapper )
        {
            _courierService = courierService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all couriers
        /// </summary>
        /// <returns>Status about getting</returns>
        /// <response code="200">Return the list of courieres</response>
        /// <response code="400">Return the error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _courierService.GetAll();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get courier by id
        /// </summary>
        /// <param name="id">Courier id</param>
        /// <returns>Status about getting courier</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _courierService.GetById(id);

                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
