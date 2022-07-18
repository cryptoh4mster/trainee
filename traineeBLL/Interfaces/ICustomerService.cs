using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traineeBLL.DTO;

namespace traineeBLL.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<IndexCustomerDTO>> GetCustomers();
        Task<IndexCustomerDTO> GetCustomerById(int id);
        Task DeleteCustomerById(int id);
        Task<CreateCustomerDTO> UpdateCustomer(CreateCustomerDTO customerDto);
        Task<CreateCustomerDTO> AddCustomer(CreateCustomerDTO customerDto);
    }
}
