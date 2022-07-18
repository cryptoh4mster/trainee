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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IndexCustomerDTO>> GetCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAll();
            IEnumerable<IndexCustomerDTO> customerDtos = _mapper.Map<IEnumerable<IndexCustomerDTO>>(customers);
            return customerDtos;
        }

        public async Task<IndexCustomerDTO> GetCustomerById(int id)
        {
            Customer customer = await _customerRepository.GetById(id);
            IndexCustomerDTO customerDto = _mapper.Map<IndexCustomerDTO>(customer);
            return customerDto;
        }

        public async Task DeleteCustomerById(int id)
        {
            await _customerRepository.Delete(id);
        }

        public async Task<CreateCustomerDTO> AddCustomer(CreateCustomerDTO customerDto)
        {
            Customer customer = _mapper.Map<Customer>(customerDto);
            Customer customerForMapping = await _customerRepository.Add(customer);
            return _mapper.Map<CreateCustomerDTO>(customerForMapping);
        }
        public async Task<CreateCustomerDTO> UpdateCustomer(CreateCustomerDTO customerDto)
        {
            Customer customer = _mapper.Map<Customer>(customerDto);
            Customer customerForMapping = await _customerRepository.Update(customer);
            return _mapper.Map<CreateCustomerDTO>(customerForMapping);
        }
    }
}
