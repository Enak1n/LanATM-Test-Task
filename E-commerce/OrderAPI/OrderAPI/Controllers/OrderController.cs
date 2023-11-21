using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Domain.DTO.Requests;
using OrderAPI.Domain.DTO.Responses;
using OrderAPI.Domain.Entities;
using OrderAPI.Domain.Exceptions;
using OrderAPI.Domain.Interfaces;
using OrderAPI.Service.Interfaces;

namespace OrderAPI.Controllers
{
    [Authorize(Policy = "AccessToOrders")]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <returns>Status about getting orders</returns>
        /// <response code="200">Return the list of all orders</response>
        /// <response code="400">Return the error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _orderService.GetAll();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get list order by id
        /// </summary>
        /// <returns>Status about getting order</returns>
        /// <response code="200">Return the order</response>
        /// <response code="400">Return the error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var order = await _orderService.GetById(id);

                var res = _mapper.Map<OrderDTOResponse>(order);
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <returns>Status about creating order</returns>
        /// <response code="200">Return the creating order</response>
        /// <response code="400">Return the error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder(OrderDTORequest order)
        {
            try
            {
                var mappingOrder = _mapper.Map<Order>(order);
                var createdOrder = await _orderService.Create(mappingOrder);

                var res = _mapper.Map<OrderDTOResponse>(createdOrder);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancel the order
        /// </summary>
        /// <returns>Status about cancelling order</returns>
        /// <response code="200">Return the cancelling order</response>
        /// <response code="404">Order not found</response>
        /// <response code="400">Return the error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            try
            {
                var order = await _orderService.Cancel(id);

                var res = _mapper.Map<OrderDTOResponse>(order);

                return Ok(res);
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Pay for order
        /// </summary>
        /// <returns>Status about paying order</returns>
        /// <response code="200">Return the order which we paid</response>
        /// <response code="404">Order not found</response>
        /// <response code="400">Return the error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsPaymented(Guid id)
        {
            try
            {
                var order = await _orderService.IsPaymented(id);

                var res = _mapper.Map<OrderDTOResponse>(order);

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
        /// Ready order
        /// </summary>
        /// <returns>Status of the order preparation</returns>
        /// <response code="200">Return the order which we prepared</response>
        /// <response code="404">Order not found</response>
        /// <response code="400">Return the error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsReady(Guid id)
        {
            try
            {
                var order = await _orderService.IsReady(id);

                var response = _mapper.Map<OrderDTOResponse>(order);

                return Ok(response);
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
        /// Receive order
        /// </summary>
        /// <returns>Status about receiveing the order</returns>
        /// <response code="200">Return the order which we received</response>
        /// <response code="404">Order not found</response>
        /// <response code="400">Return the error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsReceived(Guid id)
        {
            try
            {
                var order = await _orderService.IsReceived(id);

                var response = _mapper.Map<OrderDTOResponse>(order);

                return Ok(response);
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
