using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trainee.ViewModels;
using traineeBLL.DTO;
using traineeBLL.Interfaces;

namespace trainee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        //TODO: Везде сделать проверку на ошибки и отправку разных статусных кодов
        [HttpGet]
        [Route("orders")]
        public async Task<ActionResult<IEnumerable<IndexOrderViewModel>>> GetOrders()
        {
            IEnumerable<IndexOrderDTO> orderDtos = await _orderService.GetOrders();
            IEnumerable<IndexOrderViewModel> orderViewModels = _mapper.Map<IEnumerable<IndexOrderViewModel>>(orderDtos);
            return Ok(orderViewModels);
        }

        [HttpGet]
        [Route("orders/{id}")]
        public async Task<ActionResult<IndexOrderDTO>> GetOrderById(int id)
        {
            try
            {
                IndexOrderDTO orderDto = await _orderService.GetOrderById(id);
                IndexOrderViewModel orderViewModel = _mapper.Map<IndexOrderViewModel>(orderDto);
                return Ok(orderViewModel);
            }
            catch
            {
                return NotFound("Заказа с таким id не существует");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderViewModel>> AddOrder(CreateOrderViewModel orderViewModel)
        {
            CreateOrderDTO orderDto = _mapper.Map<CreateOrderDTO>(orderViewModel);
            return Ok(await _orderService.AddOrder(orderDto));
        }

        [HttpDelete]
        [Route("orders/{id}")]
        public async Task<ActionResult> DeleteOrderById(int id)
        {
            try
            {
                await _orderService.DeleteOrderById(id);
                return Ok();
            }
            catch
            {
                return NotFound("Заказа с таким id не существует");
            }
        }

        [HttpPut]
        public async Task<ActionResult<CreateOrderViewModel>> UpdateOrder(CreateOrderViewModel orderViewModel)
        {
            CreateOrderDTO orderDto = _mapper.Map<CreateOrderDTO>(orderViewModel);
            try
            {
                return Ok(await _orderService.UpdateOrder(orderDto));
            }
            catch
            {
                return NotFound("Сущность не найдена");
            }
        }
    }
}
