﻿using OrderAPI.Domain.Entities;
using OrderAPI.Domain.Exceptions;
using OrderAPI.Domain.Interfaces;
using OrderAPI.Service.Interfaces;

namespace OrderAPI.Service.Business
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> Cancel(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);

            if (order == null)
                throw new NotFoundException($"Order with Id {id} not found!");

            if (order.IsCanceled)
                throw new Exception("Order has been already canceled!");

            order.IsCanceled = true;

            await _unitOfWork.Orders.EditAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public async Task<Order> Create(Order order)
        {
            await _unitOfWork.Orders.AddAsync(order);

            return order;
        }

        public async Task CreateDelivery(Order order, Guid addressId)
        {
            var orderDb = _unitOfWork.Orders.GetByIdAsync(order.Id);

            if(orderDb != null)
            {
                throw new UniqueException($"Order with Id {order.Id} already exist!");
            }

            order.AddressId = addressId;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);

            if(order == null)
            {
                throw new NotFoundException($"Order with ID {id} not found!");
            }

            return order;
        }

        public async Task<Order> IsPaymented(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);

            if (order == null)
                throw new NotFoundException($"Order with ID {id} not found!");

            if(!order.IsPaymented)
                throw new Exception("Order has been already paid!");

            if (order.IsCanceled)
                throw new Exception("Order was canceled!");

            order.IsPaymented = true;

            await _unitOfWork.Orders.EditAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public async Task<Order> IsReady(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);

            if (order == null)
                throw new NotFoundException($"Order with this Id {id} not found!");

            if (order.IsCanceled)
                throw new Exception("Order has been canceled!");


            if (order.IsReady)
                throw new Exception("Order has been already readied!");

            order.IsReady = true;

            await _unitOfWork.Orders.EditAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public async Task<Order> IsReceived(Guid id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);

            if (order == null)
                throw new Exception($"Order with this Id {id} not found!");

            if (order.IsReceived)
                throw new Exception("Order has been already received!");

            if (order.IsCanceled)
                throw new Exception("Order has been canceled!");

            if (order.IsPaymented)
                throw new Exception("The order has not been paid!");

            order.IsReceived = true;

            await _unitOfWork.Orders.EditAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }
    }
}
