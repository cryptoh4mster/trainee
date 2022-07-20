using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using trainee.ViewModels;
using traineeBLL.DTO;
using traineeBLL.Interfaces;
using Microsoft.Extensions.Logging;

namespace trainee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService, IMapper mapper, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [Route("customers")]
        public async Task<ActionResult<IEnumerable<IndexCustomerViewModel>>> GetCustomers()
        {
            IEnumerable<IndexCustomerDTO> customerDtos = await _customerService.GetCustomers();
            IEnumerable<IndexCustomerViewModel> customerViewModels = _mapper.Map<IEnumerable<IndexCustomerViewModel>>(customerDtos);
            return Ok(customerViewModels);
        }

        [HttpGet]
        [Route("customers/{id}")]
        public async Task<ActionResult<IndexCustomerDTO>> GetCustomerById(int id)
        {
            try
            {
                IndexCustomerDTO customerDto = await _customerService.GetCustomerById(id);
                IndexCustomerViewModel customerViewModel = _mapper.Map<IndexCustomerViewModel>(customerDto);
                return Ok(customerViewModel);
            }
            catch
            {
                return NotFound("Заказчика с таким id не существует");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateCustomerViewModel>> AddCustomer(CreateCustomerViewModel customerViewModel)
        {
            CreateCustomerDTO customerDto = _mapper.Map<CreateCustomerDTO>(customerViewModel);
            return Ok(await _customerService.AddCustomer(customerDto));
        }

        [HttpDelete]
        [Route("customers/{id}")]
        public async Task<ActionResult> DeleteCustomerById(int id)
        {
            try
            {
                await _customerService.DeleteCustomerById(id);
                return Ok();
            }
            catch
            {
                return NotFound("Заказчика с таким id не существует");
            }
        }

        [HttpPut]
        public async Task<ActionResult<CreateCustomerViewModel>> UpdateCustomer(CreateCustomerViewModel customerViewModel)
        {
            CreateCustomerDTO customerDto = _mapper.Map<CreateCustomerDTO>(customerViewModel);
            try
            {
                return Ok(await _customerService.UpdateCustomer(customerDto));
            }
            catch
            {
                return NotFound("Сущность не найдена");
            }
        }
    }


}
