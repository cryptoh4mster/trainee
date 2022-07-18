using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using traineeBLL.DTO;
using traineeBLL.Interfaces;
using traineeDAL.Entities;
using traineeDAL.Interfaces;

namespace traineeBLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IndexOrderDTO>> GetOrders()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAll();
            IEnumerable<IndexOrderDTO> orderDtos = _mapper.Map<IEnumerable<IndexOrderDTO>>(orders);
            return orderDtos;
        }

        public async Task<IndexOrderDTO> GetOrderById(int id)
        {
            Order order = await _orderRepository.GetById(id);
            IndexOrderDTO orderDto = _mapper.Map<IndexOrderDTO>(order);
            return orderDto;
        }

        public async Task DeleteOrderById(int id)
        {
            await _orderRepository.Delete(id);
        }

        public async Task<CreateOrderDTO> AddOrder(CreateOrderDTO orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
            Order orderForMapping = await _orderRepository.Add(order);
            return _mapper.Map<CreateOrderDTO>(orderForMapping);
        }
        public async Task<CreateOrderDTO> UpdateOrder(CreateOrderDTO orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
            Order orderForMapping = await _orderRepository.Update(order);
            return _mapper.Map<CreateOrderDTO>(orderForMapping);
        }
    }
}
