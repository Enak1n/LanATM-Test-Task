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

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
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

        [HttpPut]
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

        [HttpPut]
        public async Task<IActionResult> IsPaymented(Guid id)
        {
            try
            {
                var order = await _orderService.IsReceived(id);

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

        [HttpPut]
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

        [HttpPut]
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
