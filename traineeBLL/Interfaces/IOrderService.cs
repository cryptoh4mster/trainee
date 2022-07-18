using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traineeBLL.DTO;

namespace traineeBLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<IndexOrderDTO>> GetOrders();
        Task<IndexOrderDTO> GetOrderById(int id);
        Task DeleteOrderById(int id);
        Task<CreateOrderDTO> UpdateOrder(CreateOrderDTO orderDto);
        Task<CreateOrderDTO> AddOrder(CreateOrderDTO orderDto);
    }
}
