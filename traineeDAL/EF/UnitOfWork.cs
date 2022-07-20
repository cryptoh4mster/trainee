using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using traineeDAL.Interfaces;

namespace traineeDAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TraineeDbContext _context;
        public IOrderRepository Orders { get; }
        public ICustomerRepository Customers { get; }
        public UnitOfWork(TraineeDbContext context, IOrderRepository orders, ICustomerRepository customers)
        {
            _context = context;
            Orders = orders;
            Customers = customers;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
