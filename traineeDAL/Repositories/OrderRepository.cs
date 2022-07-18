using System;
using System.Collections.Generic;
using System.Text;
using traineeDAL.EF;
using traineeDAL.Entities;
using traineeDAL.Interfaces;

namespace traineeDAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(TraineeDbContext _context) : base(_context)
        {

        }
    }
}
