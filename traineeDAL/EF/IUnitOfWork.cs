using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traineeDAL.Interfaces;

namespace traineeDAL.EF
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get;}
        ICustomerRepository Customers { get;}
        Task CompleteAsync();
    }
}
