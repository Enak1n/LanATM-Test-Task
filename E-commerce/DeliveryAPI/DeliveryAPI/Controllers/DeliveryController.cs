using AutoMapper;
using DeliveryAPI.Domain.DTO.Requests;
using DeliveryAPI.Domain.Entities;
using DeliveryAPI.Domain.Exceptions;
using DeliveryAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeliveryAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryServie;
        private readonly IMapper _mapper;

        public DeliveryController(IDeliveryService deliveryService, IMapper mapper)
        {
            _deliveryServie = deliveryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all deliveries
        /// </summary>
        /// <returns>Status about getting all deliveries</returns>
        /// <response code="200">Return the list of deliveries</response>
        /// <response code="400">Return the error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _deliveryServie.GetAll();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get delivery by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Status about getting delivery</returns>
        /// <response code="200">Return the delivery</response>
        /// <response code="400">Return the error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _deliveryServie.GetById(id);

                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new delivery
        /// </summary>
        /// <param name="deliveryDTORequest">New Delivery</param>
        /// <returns>Status about creating</returns>
        /// <response code="200">Return the new delivery</response>
        /// <response code="400">Return the error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "ChangingOfDeliveries")]
        public async Task<IActionResult> Create(DeliveryDTORequest deliveryDTORequest)
        {
            try
            {
                Delivery delivery = _mapper.Map<Delivery>(deliveryDTORequest);

                var res = await _deliveryServie.Create(delivery);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancel the delivery
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Status about cancelling delivery</returns>
        /// <response code="200">Return the canceled delivery</response>
        /// <response code="404">Return the error if delivery not found</response>
        /// <response code="400">Return the error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "ChangingOfDeliveries")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            try
            {
                var res = _deliveryServie.Cancel(id);

                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// PickUp order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Status about picking up the order</returns>
        /// <response code="200">Return the success message about delivery</response>
        /// <response code="404">Return the error if delivery not found</response>
        [HttpPut]
        [Authorize(Policy = "Courier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PickUpOrderFromWarehouse(Guid id)
        {
            try
            {
                Guid courierId = new Guid(User.FindFirstValue("UserId"));
                
                await _deliveryServie.PickUpOrderFromWarehouse(id, courierId);

                return Ok($"Courier took order {id}");
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// PickUp order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Status about return to warehouse</returns>
        /// <response code="200">Return the success message about delivery</response>
        /// <response code="404">Return the error if delivery not found</response>
        /// <response code="400">Return the error if delivery has wrong properties</response>
        [HttpPut]
        [Authorize(Policy = "Courier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReturnToWarehouse(Guid id)
        {
            try
            {
                await _deliveryServie.ReturnToWarehouse(id);

                return Ok($"Courier took back the order {id}");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
