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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IndexCustomerDTO>> GetCustomers()
        {
            IEnumerable<Customer> customers = await _unitOfWork.Customers.GetAll();
            IEnumerable<IndexCustomerDTO> customerDtos = _mapper.Map<IEnumerable<IndexCustomerDTO>>(customers);
            return customerDtos;
        }

        public async Task<IndexCustomerDTO> GetCustomerById(int id)
        {
            Customer customer = await _unitOfWork.Customers.GetById(id);
            IndexCustomerDTO customerDto = _mapper.Map<IndexCustomerDTO>(customer);
            return customerDto;
        }

        public async Task DeleteCustomerById(int id)
        {
            await _unitOfWork.Customers.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<CreateCustomerDTO> AddCustomer(CreateCustomerDTO customerDto)
        {
            Customer customer = _mapper.Map<Customer>(customerDto);
            Customer customerForMapping = await _unitOfWork.Customers.Add(customer);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CreateCustomerDTO>(customerForMapping);
        }
        public async Task<CreateCustomerDTO> UpdateCustomer(CreateCustomerDTO customerDto)
        {
            Customer customer = _mapper.Map<Customer>(customerDto);
            Customer customerForMapping = await _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CreateCustomerDTO>(customerForMapping);
        }
    }
}
