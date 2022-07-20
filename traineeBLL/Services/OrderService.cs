using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using traineeBLL.DTO;
using traineeBLL.Interfaces;
using traineeDAL.EF;
using traineeDAL.Entities;
using traineeDAL.Interfaces;

namespace traineeBLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IndexOrderDTO>> GetOrders()
        {
            IEnumerable<Order> orders = await _unitOfWork.Orders.GetAll();
            IEnumerable<IndexOrderDTO> orderDtos = _mapper.Map<IEnumerable<IndexOrderDTO>>(orders);
            return orderDtos;
        }

        public async Task<IndexOrderDTO> GetOrderById(int id)
        {
            Order order = await _unitOfWork.Orders.GetById(id);
            IndexOrderDTO orderDto = _mapper.Map<IndexOrderDTO>(order);
            return orderDto;
        }

        public async Task DeleteOrderById(int id)
        {
            await _unitOfWork.Orders.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<CreateOrderDTO> AddOrder(CreateOrderDTO orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
            Order orderForMapping = await _unitOfWork.Orders.Add(order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CreateOrderDTO>(orderForMapping);
        }
        public async Task<CreateOrderDTO> UpdateOrder(CreateOrderDTO orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
            Order orderForMapping = await _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CreateOrderDTO>(orderForMapping);
        }
    }
}
